using MediatR;

namespace Event.Application.Functions.Candidate.Events;

public class CreateCandidateApplicationEvent : IRequest<Unit>
{
    public int OfferId { get; set; }
    public int UserId { get; set; }
    public Guid UserOfferId { get; set; }
}