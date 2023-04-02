
using Common.Installers.Persistance.Contracts;

namespace Recruitment.Domain.Enities;

public class Offer : IDataEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}