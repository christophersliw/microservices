using MQ;

namespace Event.Application.Functions.Candidate.IntegrationEvents;

public class CreateCandidateApplicationIntegrationEvent : IntegrationBaseEvent
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
    
    public Guid UserOfferId { get; set; }
}