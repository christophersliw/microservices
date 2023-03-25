using Candidate.Application.Contracts.Persistence;
using Candidate.Persistence.EF.Repositories;
using Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Persistence.EF.Installers.Items;

public class PersistenceEFInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("11111111111111111111");
        
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IAsyncUserRepository, UserRepository>();
        services.AddScoped<IAsyncUserOfferRepository, UserOfferRepository>();
    }
}