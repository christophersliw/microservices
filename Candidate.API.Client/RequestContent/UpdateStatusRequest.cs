namespace Candidate.API.Client.RequestContent;

public class UpdateStatusRequest
{
    public Guid UserOfferId { get; set; }
    public int StatusId { get; set; }
}