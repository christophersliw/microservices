using MQ;

namespace Candidate.Domain.Events;

public class CreateCandidateApplicationEvent
{
    public int OfferId { get; set; }
    public int UserId { get; set; }
    
}