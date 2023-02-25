namespace Candidate.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Surrname { get; set; }
    
    public virtual ICollection<UserOffer> UserOffers { get; set; }
}