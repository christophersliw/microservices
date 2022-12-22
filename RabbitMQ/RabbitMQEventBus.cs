using System.Text;
using Autofac;
using Microsoft.Extensions.Logging;
using MQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ;

public class RabbitMQEventBus : IIntegrationEventBus
{
    private const string BROKER_NAME = "recruitment_system_event_bus";
    private const string AUTOFAC_SCOPE_NAME = "recruitment_system_event_bus";
    
    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly IRabbitMQPersistentConnection _rabbitMqPersistentConnection;
    private readonly IEventBusSubscriptionsManager _eventBusSubscriptionsManager;
    private readonly ILifetimeScope _lifetimeScope;

    public RabbitMQEventBus(
        ILogger<RabbitMQEventBus> logger, 
        IRabbitMQPersistentConnection rabbitMqPersistentConnection, 
        IEventBusSubscriptionsManager eventBusSubscriptionsManager, 
        ILifetimeScope lifetimeScope)
    {
        _logger = logger;
        _rabbitMqPersistentConnection = rabbitMqPersistentConnection;
        _eventBusSubscriptionsManager = eventBusSubscriptionsManager;
        _lifetimeScope = lifetimeScope;
    }
    public void Publish(IntegrationBaseEvent @event)
    {
        if (!_rabbitMqPersistentConnection.IsConnected)
        {
            _rabbitMqPersistentConnection.TryConnect();
        }

        _logger.LogTrace("RabbitMQEventBus > Publish - tworzymy polaczenie");

        //pobieramy routing key na podstawie typu eventa
        var eventName = @event.GetType().Name;
        
        _logger.LogTrace($"RabbitMQEventBus > Publish - pobieramy typ zdarzenia do wyslania:{eventName}");

        using (var channel = _rabbitMqPersistentConnection.CreateModel())
        {
            _logger.LogTrace($"RabbitMQEventBus > Publish - definiujemy exchange brokera");
            channel.ExchangeDeclare(exchange: BROKER_NAME, type:"direct");
            
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            
            var properties = channel.CreateBasicProperties();
            //zapisanie na dysku przed wyslaniem
            properties.DeliveryMode = 2; // persistent
            
            _logger.LogTrace($"RabbitMQEventBus > Publish - pubishing event: {@event.EventId}");
            channel.BasicPublish(exchange: 
                BROKER_NAME,
                routingKey: eventName,
                basicProperties: null,
                body: body);
        }
    }

    public void Subscribe<T, TH>() where T : IntegrationBaseEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var isEvenIsSubscribed = _eventBusSubscriptionsManager.HasSubscriptionsForEvent(eventName);

        if (!isEvenIsSubscribed)
        {
            _eventBusSubscriptionsManager.AddSubscription<T, TH>();
            
            if (!_rabbitMqPersistentConnection.IsConnected)
            {
                _rabbitMqPersistentConnection.TryConnect();
            }

            using (var channel = _rabbitMqPersistentConnection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");
                
                var queueName = channel.QueueDeclare().QueueName;
                
                channel.QueueBind(queue: queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);
                
                var consumer = new AsyncEventingBasicConsumer(channel);
                
                consumer.Received += async (model, ea) =>
                {
                    if (_eventBusSubscriptionsManager.HasSubscriptionsForEvent(eventName))
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        
                        //nalezy utworzyc wlasny scope, nie jestesmy w request, alternatywa to IServiceScopeFactory 
                        using (var scope = _lifetimeScope.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                        {
                            var subcripions = _eventBusSubscriptionsManager.GetHandlersForEvent(eventName);
                    
                            foreach (var subscriptionInfo in subcripions)
                            {
                                var handler = scope.ResolveOptional(subscriptionInfo.HandlerType);
                                
                                if (handler == null)
                                    continue;
                    
                                var eventType = _eventBusSubscriptionsManager.GetEventTypeByName(eventName);
                                var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                                
                                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                                
                                await Task.Yield();
                                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                            }
                        }
                    }
                    
                     channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                
                channel.BasicConsume(queue: queueName,
                    autoAck: false,
                    consumer: consumer);
            }
        }
    }
}