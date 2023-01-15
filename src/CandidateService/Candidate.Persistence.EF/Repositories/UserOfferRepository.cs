using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Candidate.Persistence.EF.DummyData;

namespace Candidate.Persistence.EF.Repositories;
public class UserOfferRepository : BaseRepository<UserOffer>, IAsyncUserOfferRepository
{
    public Task<IReadOnlyList<UserOffer>> GetByUserIdAsync(int userId)
    {
        return Task.FromResult<IReadOnlyList<UserOffer>>(DummyUserOffers.Get().Where(e => e.UserId == userId).ToList());
    }
    
    public override Task<UserOffer> AddAsync(UserOffer entity)
    {
        var result = DummyUserOffers.Add(entity);
        
        return Task.FromResult<UserOffer>(result);
    }
}