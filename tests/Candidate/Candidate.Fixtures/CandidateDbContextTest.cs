using Candidate.Domain.Entities;
using Candidate.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Fixtures;

public class CandidateDbContextTest : CandidateDbContext
{
    public CandidateDbContextTest(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = new Guid("B30461FE-F05E-4B65-B4C7-7FA1B45C6B0A"),
            Surrname = "Kowalski",
            FirstName = "Jan"
        });




    }
}