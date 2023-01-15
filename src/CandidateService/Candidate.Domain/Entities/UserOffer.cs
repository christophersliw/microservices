namespace Candidate.Domain.Entities;

public class UserOffer
{
    public Guid UserOfferId { get; set; }
    public int UserId { get; set; }
    public int OfferId { get; set; }
    public DateTime ApplicationDate { get; set; }
}