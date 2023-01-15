using AutoMapper;
using Candidate.Application.Responses;
using Recruitment.API.Client;

namespace Candidate.Application.Services;

public class OfferClientService : IOfferClientService
{
    private readonly IMapper _mapper;
    private readonly IOfferClient _offerClient;

    public OfferClientService(IMapper mapper, IOfferClient offerClient)
    {
        _mapper = mapper;
        _offerClient = offerClient;
    }
    
    public async Task<OfferResponse> GetById(int id, OfferResponse defaultItem, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _offerClient.Item.Get(id, cancellationToken);

            return _mapper.Map<OfferResponse>(result);
        }
        catch (Exception e)
        {
            return defaultItem;
        }
    }
}