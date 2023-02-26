using KnowHow.Application.Functions.Commands.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using MQ;

namespace KnowHow.Application.Functions.Commands.OneEventManyConsumer;

public class ExecuteOneEventManyConsumerHandler : IRequestHandler<ExecuteOneEventManyConsumerCommand, ExecuteOneEventManyConsumerResponse>
{
    private readonly ILogger<ExecuteOneEventManyConsumerHandler> _logger;
    private readonly IIntegrationEventBus _integrationEventBus;
    
    public ExecuteOneEventManyConsumerHandler(ILogger<ExecuteOneEventManyConsumerHandler> logger, IIntegrationEventBus integrationEventBus)
    {
        _logger = logger;
        _integrationEventBus = integrationEventBus;
    }
    
    public async Task<ExecuteOneEventManyConsumerResponse> Handle(ExecuteOneEventManyConsumerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ExecuteOneEventManyConsumerHandler > Handle - start");
        
        _integrationEventBus.Publish(new TestEventA()
        {
            EventId = Guid.NewGuid(),
            CreatedEventDate = DateTime.Now
        });
        
        _logger.LogInformation("ExecuteOneEventManyConsumerHandler > Handle - end");
        
        return await Task.FromResult(new ExecuteOneEventManyConsumerResponse() );
    }
}