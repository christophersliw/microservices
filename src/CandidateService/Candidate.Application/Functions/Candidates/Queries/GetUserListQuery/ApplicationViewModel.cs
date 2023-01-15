namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class ApplicationViewModel
{
    public Guid ApplicationGuid { get; set; }
    public DateTime ApplicationDate { get; set; }
    public OfferViewModel Offer { get; set; }
    
}