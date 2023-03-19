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
        
        //durable ustawiamy na true - jak rabbitmq sie zatrzyma, message nie zniknie, zostaje zapisana, dopoki nie zostanie przetworzona
        _channel.QueueDeclare(queue: _settings.EventQueue, true, false);
        
        //jeden message jednoczenie, worker nie przyjmie kolejnej wiadmosci dopoki nie przetworzy aktualnej, przydatne jak mamy wiele workerow
        //  channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        _logger.LogInformation("CandidateApplicationBackgroundService > ExecuteAsync > end QueueDeclare");
        
        
        //jednoczenie zostanie dostarczony tylko jeden msq, dopiero jak worker przerobi dostanie dostarczony kolejny. Mozemy uruchomic dwie instancje mikrosoerwisu z backgroud taskiem
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += async (sender, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            
                var @event = JsonConvert.DeserializeObject<CreateCandidateApplicationEvent>(message);
        
                _logger.LogInformation($"CandidateApplicationBackgroundService > ExecuteAsync > Received {message}");

                for (int i = 0; i < 2; i++)
                {
                    _logger.LogInformation($"CandidateApplicationBackgroundService > ExecuteAsync > Received - processing................");
                    Thread.Sleep(1000);
                }
           
            
                await _mediator.Send(@event, cancellationToken);
           
                _logger.LogInformation($"CandidateApplicationBackgroundService > ExecuteAsync > Received - END processing. Kandydat dodany");
                
                //wysylamy odpowiedz ze zadanie zostalo przetworzone, jezeli zadanie nie zostalo przetworzne, chwyci to drugi aktywny worker.
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"CandidateApplicationBackgroundService > ExecuteAsync > Received - Exception:{e.Message}, InnerException:{e.InnerException?.Message}");
            }
        };
        //autoAck ustawiamy na false
        _channel.BasicConsume(_settings.EventQueue, false, consumer);
        
        return Task.CompletedTask;
    }
}