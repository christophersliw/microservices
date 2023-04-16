namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class ApplicationQueryResponse
{
    public Guid ApplicationId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public OfferQueryResponse Offer { get; set; }
    
}