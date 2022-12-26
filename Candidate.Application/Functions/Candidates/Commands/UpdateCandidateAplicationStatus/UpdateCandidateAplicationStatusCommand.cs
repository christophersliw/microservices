using MediatR;

namespace Candidate.Application.Functions.Candidates.Commands;

public class UpdateCandidateAplicationStatusCommand : IRequest<UpdateCandidateAplicationStatusResponse>
{
    public int CandidateOfferId { get; set; }
    public int StatusId { get; set; }
}