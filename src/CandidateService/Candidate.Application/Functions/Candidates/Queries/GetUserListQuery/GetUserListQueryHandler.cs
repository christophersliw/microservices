using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Application.Responses;
using Candidate.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncUserRepository _asyncUserRepository;
    private readonly IAsyncUserOfferRepository _asyncUserOfferRepository;
    private readonly IOfferClientService _offerClientService;
    private readonly ILogger<GetUserListQueryHandler> _logger;

    public GetUserListQueryHandler(
        IMapper mapper,
        IAsyncUserRepository asyncUserRepository,
        IAsyncUserOfferRepository asyncUserOfferRepository,
        IOfferClientService offerClientService,
        ILogger<GetUserListQueryHandler> logger)
    {
        _mapper = mapper;
        _asyncUserRepository = asyncUserRepository;
        _asyncUserOfferRepository = asyncUserOfferRepository;
        _offerClientService = offerClientService;
        _logger = logger;
    }
    
    public async Task<List<UserQueryResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("start - GetUserListQueryHandler > Handle");
        
        var userList = await _asyncUserRepository.GetAllAsync();

        var result = _mapper.Map<List<UserQueryResponse>>(userList);

        foreach (var user in result)
        {
            var userApplicationList = await _asyncUserOfferRepository.GetQuery().Where(e => e.UserId == user.UserId).ToListAsync();

            if (userApplicationList.Any())
            {
                user.ApplicationList = new List<ApplicationQueryResponse>();

                _logger.LogInformation("GetUserListQueryHandler > Handle - start connect to another microservice");
                var offerTasks = userApplicationList.Select(async e => await _offerClientService.GetById(e.OfferId, new OfferResponse()
                {
                    OfferId = e.Id
                }, cancellationToken));

                var offers = await Task.WhenAll(offerTasks);
                
                _logger.LogInformation("GetUserListQueryHandler > Handle - finish connect to another microservice");
                
                foreach (var userOffer in userApplicationList)
                {
                    ApplicationQueryResponse applicationViewModel = new ApplicationQueryResponse()
                    {
                        ApplicationDate = userOffer.ApplicationDate,
                        ApplicationGuid = userOffer.Id
                    };

                    var offerResponse = offers.FirstOrDefault(e => e.OfferId == userOffer.OfferId);

                    if (offerResponse != null)
                    {
                        applicationViewModel.Offer = _mapper.Map<OfferQueryReponse>(offerResponse);
                    }
                    
                    user.ApplicationList.Add(applicationViewModel);
                }
            }
        }
        
        return result;
    }
}