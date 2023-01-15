using MQ;

namespace Candidate.Application.IntegrationEvents;

public class CreateCandidateApplicationIntegrationEvent : IntegrationBaseEvent
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
    
    public Guid UserOfferId { get; set; }
}