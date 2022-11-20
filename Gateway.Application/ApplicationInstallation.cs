using Gateway.Application.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Application;

public static class ApplicationInstallation
{
    public static IServiceCollection AddCandidateApplication(this IServiceCollection services, Uri candidateServiceUrl)
    {
        services.AddHttpClient<ICandidateClient, CandidateClient>(client => { client.BaseAddress = candidateServiceUrl; });
        
        return services;
    }
}