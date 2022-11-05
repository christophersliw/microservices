using System.Text;
using System.Text.Json.Serialization;
using Candidate.Application.Configurations;
using Candidate.Application.Functions.Candidates.Events;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Candidate.Application.BackgroundServices;

public class CandidateApplicationBackgroundService : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly EventBusSettings _settings;
    private readonly IModel _channel;

    public CandidateApplicationBackgroundService(IMediator mediator, ConnectionFactory connectionFactory, EventBusSettings settings)
    {
        _mediator = mediator;
        _settings = settings;

        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel.QueueDeclare(queue: _settings.EventQueue, true, false);
        
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            var @event = JsonConvert.DeserializeObject<CreateCandidateApplicationEvent>(message);

           await _mediator.Send(@event, stoppingToken);
           
           _channel.BasicAck(ea.DeliveryTag, false);
        };
        
        _channel.BasicConsume(_settings.EventQueue, false, consumer);
        
        return Task.CompletedTask;
    }
}