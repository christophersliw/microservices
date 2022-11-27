using Candidate.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Candidate.Application;

public static class EventBusInstallation
{
    public static IServiceCollection AddEventBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection("EventBus").Get<EventBusSettings>();

        services.AddSingleton(eventBusSettings);

        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = eventBusSettings.HostName
        };

        services.AddSingleton(factory);
        
        return services;
    }
}