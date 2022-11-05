using MediatR;

namespace Candidate.Application.Functions.Candidates.Events;

public class CreateCandidateApplicationEvent : IRequest<Unit>
{
    public int OfferId { get; set; }
    public int UserId { get; set; }
}