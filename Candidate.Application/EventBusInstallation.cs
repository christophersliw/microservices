using Autofac;
using Candidate.Application.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ;
using RabbitMQ;
using RabbitMQ.Client;

namespace Candidate.Application;

public static class EventBusInstallation
{
    //konfiguracja dla kolejki wykorzystywanej przez background service
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection("EventBus").Get<EventBusSettings>();

        services.AddSingleton(eventBusSettings);

        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = eventBusSettings.HostName
        };

        services.AddSingleton(factory);

        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
        
        return services;
    }
    
    //konfiguracja servisow dla kolejki wykorzystywanej przez integration events
    public static IServiceCollection AddIntegrationEventBusServices(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection("EventIntegrationBus").Get<EventBusSettings>();
        
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = eventBusSettings.HostName,
                DispatchConsumersAsync = true
            };
            
            return new RabbitMQPersistentConnection(factory, logger);
        });


        return services;
    }
    
    //konfiguracja dla kolejki wykorzystywanej przez integration events
    public static IServiceCollection AddIntegrationEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IIntegrationEventBus, RabbitMQEventBus>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            
            return new RabbitMQEventBus(logger, rabbitMQPersistentConnection, eventBusSubcriptionsManager,
                iLifetimeScope);

        });
        
        return services;
    }
}