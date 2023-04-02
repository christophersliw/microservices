using Common.Installers.Persistance;
using Common.Installers.Persistance.Contracts;
using Recruitment.Domain.Enities;
using Recruitment.Persistence.EF.Persistance;

namespace Recruitment.Persistence.EF.Repositories;

public class OfferRepository : BaseRepository<Offer, RecruitmentDbContext>, IAsyncOfferRepository
{
    public OfferRepository(RecruitmentDbContext dbContext) : base(dbContext)
    {
    }
}