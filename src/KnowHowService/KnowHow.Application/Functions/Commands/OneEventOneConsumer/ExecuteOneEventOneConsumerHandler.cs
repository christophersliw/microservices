using KnowHow.Application.Functions.Commands.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using MQ;

namespace KnowHow.Application.Functions.Commands.OneEventOneConsumer;

public class ExecuteOneEventOneConsumerHandler : IRequestHandler<ExecuteOneEventOneConsumerCommand, ExecuteOneEventOneConsumerResponse>
{
    private readonly ILogger<ExecuteOneEventOneConsumerHandler> _logger;
    private readonly IIntegrationEventBus _integrationEventBus;
    
    public ExecuteOneEventOneConsumerHandler(ILogger<ExecuteOneEventOneConsumerHandler> logger, IIntegrationEventBus integrationEventBus)
    {
        _logger = logger;
        _integrationEventBus = integrationEventBus;
    }
    
    public async Task<ExecuteOneEventOneConsumerResponse> Handle(ExecuteOneEventOneConsumerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ExecuteOneEventOneConsumerHandler > Handle - start");
        
        _integrationEventBus.Publish(new TestEventB()
        {
            EventId = Guid.NewGuid(),
            CreatedEventDate = DateTime.Now
        });
        
        _logger.LogInformation("ExecuteOneEventOneConsumerHandler > Handle - end");
        
        return await Task.FromResult(new ExecuteOneEventOneConsumerResponse() );
    }
}