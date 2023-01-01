using Event.Application.Configurations;
using Event.Application.Functions.Candidate.IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ;
using RabbitMQ;
using RabbitMQ.Client;

namespace Event.Application;

public static class EventBusInstallation
{
    
    //konfiguracja servisow dla kolejki wykorzystywanej przez integration events
    public static IServiceCollection AddIntegrationEventBusServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
        
        var eventBusSettings = configuration.GetSection("EventIntegrationBus").Get<EventBusSettings>();
        
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = eventBusSettings.HostName,
              //  DispatchConsumersAsync = true
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
            var iLifetimeScope = sp.GetRequiredService<IServiceProvider>();
            
            return new RabbitMQEventBus(logger, rabbitMQPersistentConnection, eventBusSubcriptionsManager,
                iLifetimeScope);

        });
        
        return services;
    }
    public static IServiceCollection AddIntegrationEventBusHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<CreateCandidateApplicationIntegrationEventHandler>();
        
        return services;
    }
}