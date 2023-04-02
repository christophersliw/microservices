using Common.Installers.Persistance.Contracts;

namespace Candidate.Domain.Entities;

public class User : IDataEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Surrname { get; set; }
    
    public virtual ICollection<UserOffer> UserOffers { get; set; }
}