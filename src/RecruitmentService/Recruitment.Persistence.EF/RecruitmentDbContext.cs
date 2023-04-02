using Microsoft.EntityFrameworkCore;
using Recruitment.Domain.Enities;

namespace Recruitment.Persistence.EF;

public class RecruitmentDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "recruitment";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

        modelBuilder.ApplyConfiguration(new OfferConfiguration());
    }
    
    public DbSet<Offer> Offers { get; set; }
}