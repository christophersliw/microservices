using Microsoft.Extensions.DependencyInjection;
using Recruitment.Persistence.EF.Persistance;
using Recruitment.Persistence.EF.Repositories;

namespace Recruitment.Persistence.EF;

public static class PersistanceEFInstallation
{
    public static IServiceCollection AddPersistanceEFServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IAsyncOfferRepository, OfferRepository>();
        
        return services;
    }
}