using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Candidate.Persistence.EF.DummyData;

namespace Candidate.Persistence.EF.Repositories;

public class UserRepository : BaseRepository<User>, IAsyncUserRepository
{
    public new Task<IReadOnlyList<User>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<User>>(DummyUsers.Get());
    }
}