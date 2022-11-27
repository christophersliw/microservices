using System.Text;
using System.Text.Json.Serialization;
using Candidate.Application.Configurations;
using Candidate.Application.Functions.Candidates.Events;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Candidate.Application.BackgroundServices;

public class CandidateApplicationBackgroundService : BackgroundService
{
    private readonly ILogger<CandidateApplicationBackgroundService> _logger;
    private readonly IMediator _mediator;
    private readonly EventBusSettings _settings;
    private readonly IModel _channel;

    public CandidateApplicationBackgroundService(IMediator mediator, ConnectionFactory connectionFactory, EventBusSettings settings, ILogger<CandidateApplicationBackgroundService> logger)
    {
        _logger = logger;
        _mediator = mediator;
        _settings = settings;

        try
        {
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
            
            _logger.LogInformation("CandidateApplicationBackgroundService > ctor > connection success");
        }
        catch (Exception e)
        {
          _logger.LogWarning("CandidateApplicationBackgroundService > ctor > connection failed:{message}", e.Message);
        }
    }
    
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        _logger.LogInformation("CandidateApplicationBackgroundService > ExecuteAsync > start QueueDeclare");
        
        _channel.QueueDeclare(queue: _settings.EventQueue, true, false);
        
        _logger.LogInformation("CandidateApplicationBackgroundService > ExecuteAsync > end QueueDeclare");
        
        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            var @event = JsonConvert.DeserializeObject<CreateCandidateApplicationEvent>(message);
        
            _logger.LogInformation($"CandidateApplicationBackgroundService > ExecuteAsync > Received {message}");
            
           await _mediator.Send(@event, cancellationToken);
           
           _channel.BasicAck(ea.DeliveryTag, false);
        };
        
        _channel.BasicConsume(_settings.EventQueue, false, consumer);
        
        return Task.CompletedTask;
    }
}