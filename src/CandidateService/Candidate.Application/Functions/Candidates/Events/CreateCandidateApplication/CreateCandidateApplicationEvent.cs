using MediatR;

namespace Candidate.Application.Functions.Candidates.Events;

public class CreateCandidateApplicationEvent : IRequest<Unit>
{
    public Guid OfferId { get; set; }
    public Guid UserId { get; set; }
    
    public Guid UserOfferId { get; set; }
}