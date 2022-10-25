using Candidate.Application.Contracts.Persistence;
using Candidate.Persistence.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Persistence.EF;

public static class PersistanceEFInstallation
{
    public static IServiceCollection AddPersistanceEFServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IAsyncUserRepository, UserRepository>();
        return services;
    }
}