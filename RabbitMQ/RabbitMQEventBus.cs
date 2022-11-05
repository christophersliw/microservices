using System.Text;
using MQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    public void Publish(BaseEvent @event)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "task_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                routingKey: "task_queue",
                basicProperties: properties,
                body: body);

        }
    }
}