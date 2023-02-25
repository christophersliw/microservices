using Candidate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Candidate.Persistence.EF;

public class CandidateDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "candidate";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserOfferConfiguration());
    }

    public DbSet<User> User { get; set; }
    public DbSet<UserOffer> UserOffer { get; set; }
}