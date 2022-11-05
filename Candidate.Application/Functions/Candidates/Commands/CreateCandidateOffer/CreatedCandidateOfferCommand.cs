using MediatR;

namespace Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;

public class CreatedCandidateOfferCommand : IRequest<CreatedCandidateOfferCommandResponse>
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
}