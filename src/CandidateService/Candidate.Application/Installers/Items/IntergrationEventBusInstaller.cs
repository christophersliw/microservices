using Candidate.Application.Configurations;
using Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ;
using RabbitMQ;
using RabbitMQ.Client;

namespace Candidate.Application.Installers;

public class IntergrationEventBusInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
        
        var eventBusSettings = configuration.GetSection("EventIntegrationBus").Get<EventBusSettings>();
        
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = eventBusSettings.HostName,
                // DispatchConsumersAsync = true
            };
            
            return new RabbitMQPersistentConnection(factory, logger);
        });
        
        services.AddSingleton<IIntegrationEventBus, RabbitMQEventBus>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var iLifetimeScope = sp.GetRequiredService<IServiceProvider>();
            
            return new RabbitMQEventBus(logger, rabbitMQPersistentConnection, eventBusSubcriptionsManager,
                iLifetimeScope);

        });

    }
}