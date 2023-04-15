using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Recruitment.Persistence.EF;

namespace Recruitment.Migrations;

public class DbContextFactory : IDesignTimeDbContextFactory<RecruitmentDbContext>
{
    public RecruitmentDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RecruitmentDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Recruitment.Migrations"));

        return new RecruitmentDbContext(optionsBuilder.Options);
    }
}