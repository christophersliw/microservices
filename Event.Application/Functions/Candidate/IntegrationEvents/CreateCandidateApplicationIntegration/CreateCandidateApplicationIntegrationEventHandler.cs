using Event.Application.Functions.Candidate.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using MQ;

namespace Event.Application.Functions.Candidate.IntegrationEvents;

public class CreateCandidateApplicationIntegrationEventHandler : IIntegrationEventHandler<CreateCandidateApplicationIntegrationEvent>
{
    private readonly ILogger<CreateCandidateApplicationIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;
    
    public CreateCandidateApplicationIntegrationEventHandler(IMediator mediator, ILogger<CreateCandidateApplicationIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(CreateCandidateApplicationIntegrationEvent @event)
    {
        _logger.LogInformation("CreateCandidateApplicationIntegrationEventHandler > Handle");

        await  _mediator.Send(new CreateCandidateApplicationEvent()
        {
            OfferId = @event.OfferId,
            UserId = @event.UserId,
            UserOfferId = @event.UserOfferId
        });
    }
}