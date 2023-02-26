using Microsoft.Extensions.Logging;
using MQ;

namespace KnowHow.Application.Functions.Commands.IntegrationEvents;

public class TestEventAHandler3 : IIntegrationEventHandler<TestEventA>
{
    private readonly ILogger<TestEventAHandler3> _logger;

    public TestEventAHandler3(ILogger<TestEventAHandler3> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(TestEventA @event)
    {
        _logger.LogInformation("start TestEventAHandler3 > Handle");
        
        for (int i = 0; i < 10; i++)
        {
            _logger.LogInformation($"TestEventAHandler3 > processing................");
            Thread.Sleep(1000);
        }

        _logger.LogInformation("end TestEventAHandler3 > Handle");
        
        return Task.CompletedTask;
    }
}