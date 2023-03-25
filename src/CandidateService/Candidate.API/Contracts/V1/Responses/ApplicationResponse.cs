namespace Candidate.API.Contracts.V1.Responses;

public class ApplicationResponse
{
    public Guid ApplicationGuid { get; set; }
    public DateTime ApplicationDate { get; set; }
    public OfferResponse Offer { get; set; }
}