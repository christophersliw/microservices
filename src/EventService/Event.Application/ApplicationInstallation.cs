using System.Reflection;
using Candidate.API.Client;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Application;

public static class ApplicationInstallation
{
    public static IServiceCollection AddEventApplication(this IServiceCollection services, Uri candidateServiceUri)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddHttpClient<ICandidateClient, CandidateClient>(client => { client.BaseAddress = candidateServiceUri; });

        return services;
    }
}