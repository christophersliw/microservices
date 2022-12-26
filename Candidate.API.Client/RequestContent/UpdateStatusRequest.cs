namespace Candidate.API.Client.RequestContent;

public class UpdateStatusRequest
{
    public int CandidateOfferId { get; set; }
    public int StatusId { get; set; }
}