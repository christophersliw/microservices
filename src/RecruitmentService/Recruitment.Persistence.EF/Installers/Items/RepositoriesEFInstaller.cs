using Common.Installers;
using Common.Installers.Persistance;
using Common.Installers.Persistance.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recruitment.Persistence.EF.Persistance;
using Recruitment.Persistence.EF.Repositories;

namespace Recruitment.Persistence.EF.Installers;

public class RepositoriesEFInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<,>));
        services.AddScoped<IAsyncOfferRepository, OfferRepository>();
    }
}