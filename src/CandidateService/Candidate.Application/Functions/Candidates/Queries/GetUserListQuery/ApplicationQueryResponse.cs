namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class ApplicationQueryResponse
{
    public Guid ApplicationGuid { get; set; }
    public DateTime ApplicationDate { get; set; }
    public OfferQueryReponse Offer { get; set; }
    
}