using MQ;

namespace Candidate.Domain.Events;

public class CreateCandidateApplicationEvent
{
    public Guid OfferId { get; set; }
    public Guid UserId { get; set; }
    
}