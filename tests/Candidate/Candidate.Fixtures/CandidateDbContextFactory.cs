using Microsoft.EntityFrameworkCore;

namespace Candidate.Fixtures;

public class CandidateDbContextFactory
{
    public readonly CandidateDbContextTest CandidateDbContextTestInstance;

    public CandidateDbContextFactory()
    {
        var contextOptions = new
                DbContextOptionsBuilder()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        
        
        var context = new CandidateDbContextTest(contextOptions);
        context.Database.EnsureCreated();
        CandidateDbContextTestInstance = context;
    }
}