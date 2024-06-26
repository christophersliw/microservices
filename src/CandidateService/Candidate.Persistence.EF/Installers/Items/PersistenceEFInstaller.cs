using Candidate.Application.Contracts.Persistence;
using Candidate.Persistence.EF.Repositories;
using Common.Installers;
using Common.Installers.Persistance;
using Common.Installers.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Persistence.EF.Installers.Items;

public class PersistenceEFInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
     //   services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<,>));

       services.AddScoped<IAsyncUserRepository, UserRepository>();
        services.AddScoped<IAsyncUserOfferRepository, UserOfferRepository>();
        
        services.AddDbContext<CandidateDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}