using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Common.Installers.Persistance;


namespace Candidate.Persistence.EF.Repositories;
public class UserOfferRepository : BaseRepository<UserOffer, CandidateDbContext>, IAsyncUserOfferRepository
{
    public UserOfferRepository(CandidateDbContext dbContext) : base(dbContext)
    {
    }
}