using MQ;

namespace Candidate.Application.IntegrationEvents;

public class CreateCandidateApplicationIntegrationEvent : IntegrationBaseEvent
{
    public Guid UserId { get; set; }
    public Guid OfferId { get; set; }
    public Guid UserOfferId { get; set; }
}