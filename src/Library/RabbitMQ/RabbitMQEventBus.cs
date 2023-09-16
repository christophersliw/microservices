using System.Text;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ;

public class RabbitMQEventBus : IIntegrationEventBus
{
    private const string BROKER_NAME = "recruitment_system_event_bus";
    private const string DEFAULT_QUEUE_NAME = "recruitment_system_event_queu";

    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly IRabbitMQPersistentConnection _rabbitMqPersistentConnection;
    private readonly IEventBusSubscriptionsManager _eventBusSubscriptionsManager;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMQEventBus(
        ILogger<RabbitMQEventBus> logger,
        IRabbitMQPersistentConnection rabbitMqPersistentConnection,
        IEventBusSubscriptionsManager eventBusSubscriptionsManager,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _rabbitMqPersistentConnection = rabbitMqPersistentConnection;
        _eventBusSubscriptionsManager = eventBusSubscriptionsManager;
        _serviceProvider = serviceProvider;
    }

    public void Publish(IntegrationBaseEvent @event)
    {
        if (!_rabbitMqPersistentConnection.IsConnected)
        {
            _rabbitMqPersistentConnection.TryConnect();
        }

        _logger.LogInformation("RabbitMQEventBus > Publish - tworzymy polaczenie");

        //pobieramy routing key na podstawie typu eventa
        var eventName = @event.GetType().Name;

        _logger.LogInformation($"RabbitMQEventBus > Publish - pobieramy typ zdarzenia do wyslania:{eventName}");

        using (var channel = _rabbitMqPersistentConnection.CreateModel())
        {
            _logger.LogInformation($"RabbitMQEventBus > Publish - definiujemy exchange brokera");
            //durable = true - kolejka nie zostanie usunięta po zatrzymaniu uslugi rabbita
            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            //zapisanie na dysku przed wyslaniem
            //zabezpieczenie na wypadek wylaczenia uslugi rabbita
            //message nie zostanie usuniety
            // properties.Persistent = true; 
            properties.DeliveryMode = 2; // persistent

            _logger.LogInformation($"RabbitMQEventBus > Publish - pubishing event: {@event.EventId}");
            channel.BasicPublish(exchange:
                BROKER_NAME,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body);
        }
    }

    public void Subscribe<T, TH>() where T : IntegrationBaseEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;

        _logger.LogInformation($"RabbitMQEventBus > Subscribe - sprawdzanie czy jest subskrypcja po typ{eventName}");
        var isEvenIsSubscribed = _eventBusSubscriptionsManager.HasSubscriptionsForEvent(eventName);

        if (!isEvenIsSubscribed)
        {
            _logger.LogInformation($"RabbitMQEventBus > Subscribe - rejestracja subskrypcji po typ {eventName}");
            _eventBusSubscriptionsManager.AddSubscription<T, TH>();

            if (!_rabbitMqPersistentConnection.IsConnected)
            {
                _rabbitMqPersistentConnection.TryConnect();
            }

            var channel = _rabbitMqPersistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            // var queueName = channel.QueueDeclare().QueueName;

            //uzyjemy stalej nazwy kolejki
            var queueName = DEFAULT_QUEUE_NAME;

            _logger.LogInformation($"RabbitMQEventBus > Subscribe - deklaracja kolejki: ");

            //durable = true - kolejka nie zostanie usunięta po zatrzymaniu uslugi rabbita
            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(queue: queueName,
                exchange: BROKER_NAME,
                routingKey: eventName);

            var consumer = new EventingBasicConsumer(channel);
            
  
            
            consumer.Received += async (model, ea) =>
            {
                _logger.LogInformation($"RabbitMQEventBus > Subscribe - Received!!!");
                if (_eventBusSubscriptionsManager.HasSubscriptionsForEvent(eventName))
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    //nalezy utworzyc wlasny scope, nie jestesmy w request,
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var subcripions = _eventBusSubscriptionsManager.GetHandlersForEvent(eventName);

                        foreach (var subscriptionInfo in subcripions)
                        {
                            var handler = scope.ServiceProvider.GetRequiredService(subscriptionInfo.HandlerType);
            

                            if (handler == null)
                                continue;

                            var eventType = _eventBusSubscriptionsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            //gwarancja tego ze w przypadku, gdy metoda Handle bedzie synchroniczna, to na chwile wyskoczymy
                            //aby bylo mozna przetworzyc kolejny element z kolejki
                            //allow caller to continue while waiting for asynchronous operation
                            await Task.Yield();
                            await (Task) concreteType.GetMethod("Handle")
                                .Invoke(handler, new object[] {integrationEvent});
                        }
                    }
                }

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _logger.LogInformation($"RabbitMQEventBus > Subscribe - rozpoczynamy subskrypcje");
            //autoAck: false - potwierdzenie przetworzenie message nie wyjdzie automatycznie,
            //kolejka usunie message dopiero jak dostanie info o zakonczeniu: channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            //jest to zabezpecznie na wypadek wylaczenia usługi przetwarzajacej message
            channel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);
        }
    }
}