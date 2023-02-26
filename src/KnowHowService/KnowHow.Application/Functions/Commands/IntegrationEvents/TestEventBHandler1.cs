using Microsoft.Extensions.Logging;
using MQ;

namespace KnowHow.Application.Functions.Commands.IntegrationEvents;

public class TestEventBHandler1 : IIntegrationEventHandler<TestEventB>
{
    private readonly ILogger<TestEventBHandler1> _logger;

    public TestEventBHandler1(ILogger<TestEventBHandler1> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(TestEventB @event)
    {
        _logger.LogInformation("start TestEventBHandler1 > Handle");
        
        for (int i = 0; i < 10; i++)
        {
            _logger.LogInformation($"TestEventBHandler1 > processing................");
            Thread.Sleep(1000);
        }

        _logger.LogInformation("end TestEventBHandler1 > Handle");
        
        return Task.CompletedTask;
    }
}