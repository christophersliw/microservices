using Common.Installers.Persistance.Contracts;
using Recruitment.Domain.Enities;

namespace Recruitment.Persistence.EF.Persistance;

public interface IAsyncOfferRepository : IAsyncRepository<Offer>, IRepository<Offer>
{
    
}