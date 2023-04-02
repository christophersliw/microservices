using Common.Installers.Persistance.Contracts;

namespace Candidate.Domain.Entities;

public class UserOffer : IDataEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Guid OfferId { get; set; }
    public DateTime ApplicationDate { get; set; }
}