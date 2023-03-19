using MediatR;

namespace Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;

public class CreatedCandidateOfferCommand : IRequest<CreatedCandidateOfferCommandResponse>
{
    public Guid UserId { get; set; }
    public Guid OfferId { get; set; }
}