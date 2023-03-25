namespace Candidate.API.Contracts.V1.Requests;

public class CreateCandidateApplicationRequest
{
    public Guid UserId { get; set; }
    public Guid OfferId { get; set; }
}