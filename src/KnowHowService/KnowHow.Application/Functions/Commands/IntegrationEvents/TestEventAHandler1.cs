using Microsoft.Extensions.Logging;
using MQ;

namespace KnowHow.Application.Functions.Commands.IntegrationEvents;

public class TestEventAHandler1 : IIntegrationEventHandler<TestEventA>
{
    private readonly ILogger<TestEventAHandler1> _logger;

    public TestEventAHandler1(ILogger<TestEventAHandler1> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(TestEventA @event)
    {
        _logger.LogInformation("start TestEventAHandler1 > Handle");
        
        for (int i = 0; i < 10; i++)
        {
            _logger.LogInformation($"TestEventAHandler1 > processing................");
            Thread.Sleep(1000);
        }

        _logger.LogInformation("end TestEventAHandler1 > Handle");
        
        return Task.CompletedTask;
    }
}