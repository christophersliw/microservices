using Candidate.Application.Configurations;
using Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Candidate.Application.Installers;

//konfiguracja dla kolejki wykorzystywanej przez background service
public class BackgroundServiceEventBusInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection("EventBus").Get<EventBusSettings>();

        services.AddSingleton(eventBusSettings);

        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = eventBusSettings.HostName,
            Port= 5672,
            UserName = "guest",
            Password = "guest"
        };

        services.AddSingleton(factory);
    }
}