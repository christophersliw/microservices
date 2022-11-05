using Candidate.Domain.Events;
using MediatR;
using MQ;

namespace Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;

public class CreatedCandidateOfferCommandHandler : IRequestHandler<CreatedCandidateOfferCommand, CreatedCandidateOfferCommandResponse>
{
    private readonly IEventBus _eventBus;

    public CreatedCandidateOfferCommandHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public async Task<CreatedCandidateOfferCommandResponse> Handle(CreatedCandidateOfferCommand request, CancellationToken cancellationToken)
    {
       _eventBus.Publish(new CreateCandidateApplicationEvent(){OfferId = request.OfferId, UserId = request.UserId});

       return await Task.FromResult(new CreatedCandidateOfferCommandResponse());
    }
}                    