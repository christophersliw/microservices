using AutoMapper;
using MediatR;
using Recruitment.Persistence.EF.Persistance;

namespace Recruitment.Application.Functions;

public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, OfferViewModel>
{
    private readonly IMapper _mapper;
    private readonly IAsyncOfferRepository _asyncOfferRepository;

    public GetOfferByIdQueryHandler(IMapper mapper, IAsyncOfferRepository asyncOfferRepository)
    {
        _mapper = mapper;
        _asyncOfferRepository = asyncOfferRepository;
    }
    
    public async Task<OfferViewModel> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var offer = await _asyncOfferRepository.GetByIdAsync(request.OfferId);

        return _mapper.Map<OfferViewModel>(offer);
    }
}