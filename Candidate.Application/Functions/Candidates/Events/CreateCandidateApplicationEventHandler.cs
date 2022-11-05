using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using MediatR;

namespace Candidate.Application.Functions.Candidates.Events;

public class CreateCandidateApplicationEventHandler : IRequestHandler<CreateCandidateApplicationEvent>
{
    private readonly IMapper _mapper;
    private readonly IAsyncUserOfferRepository _asyncUserOfferRepository;

    public CreateCandidateApplicationEventHandler(IMapper mapper, IAsyncUserOfferRepository asyncUserOfferRepository)
    {
        _mapper = mapper;
        _asyncUserOfferRepository = asyncUserOfferRepository;
    }
    
    public async Task<Unit> Handle(CreateCandidateApplicationEvent request, CancellationToken cancellationToken)
    {
        var userOfferEntity = _mapper.Map<UserOffer>(request);
        userOfferEntity.ApplicationDate = DateTime.Now;

        await _asyncUserOfferRepository.AddAsync(userOfferEntity);
        
        return Unit.Value;
    }
}