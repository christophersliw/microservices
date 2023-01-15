using Recruitment.Domain.Enities;
using Recruitment.Persistence.EF.DummyData;
using Recruitment.Persistence.EF.Persistance;

namespace Recruitment.Persistence.EF.Repositories;

public class OfferRepository : BaseRepository<Offer>, IAsyncOfferRepository
{
    public Task<Offer?> GetByIdAsync(int id)
    {
        return Task.FromResult(DummyOffer.Get().FirstOrDefault(e => e.OfferId == id));
    }

}