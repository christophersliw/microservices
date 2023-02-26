using Microsoft.Extensions.Logging;
using MQ;

namespace KnowHow.Application.Functions.Commands.IntegrationEvents;

public class TestEventAHandler2 : IIntegrationEventHandler<TestEventA>
{
    private readonly ILogger<TestEventAHandler2> _logger;

    public TestEventAHandler2(ILogger<TestEventAHandler2> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(TestEventA @event)
    {
        _logger.LogInformation("start TestEventAHandler2 > Handle");
        
        for (int i = 0; i < 10; i++)
        {
            _logger.LogInformation($"TestEventAHandler2 > processing................");
            Thread.Sleep(1000);
        }

        _logger.LogInformation("end TestEventAHandler2 > Handle");
        
        return Task.CompletedTask;
    }
}