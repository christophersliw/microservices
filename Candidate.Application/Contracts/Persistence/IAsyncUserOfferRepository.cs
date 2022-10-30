using Candidate.Domain.Entities;

namespace Candidate.Application.Contracts.Persistence;

public interface IAsyncUserOfferRepository : IAsyncRepository<UserOffer>
{
    Task<IReadOnlyList<UserOffer>> GetByUserIdAsync(int userId);
}