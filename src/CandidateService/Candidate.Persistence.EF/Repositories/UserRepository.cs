using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using Candidate.Persistence.EF.DummyData;
using Common.Installers.Persistance;

namespace Candidate.Persistence.EF.Repositories;

public class UserRepository : BaseRepository<User, CandidateDbContext>, IAsyncUserRepository
{
    public UserRepository(CandidateDbContext dbContext) : base(dbContext)
    {
    }
}