using System.Text;
using Candidate.Application.Configurations;
using Candidate.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using MQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;

public class CreatedCandidateOfferCommandHandler : IRequestHandler<CreatedCandidateOfferCommand, CreatedCandidateOfferCommandResponse>
{
    private readonly ILogger<CreatedCandidateOfferCommandHandler> _logger;
    private readonly ConnectionFactory _connectionFactory;
    private readonly EventBusSettings _eventBusSettings;

    public CreatedCandidateOfferCommandHandler(ILogger<CreatedCandidateOfferCommandHandler> logger, ConnectionFactory connectionFactory, EventBusSettings eventBusSettings)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
        _eventBusSettings = eventBusSettings;
    }
    public async Task<CreatedCandidateOfferCommandResponse> Handle(CreatedCandidateOfferCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreatedCandidateOfferCommandHandler > Handle - start");
            
            var connection = _connectionFactory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                //durable ustawiamy na true - jak rabbitmq sie zatrzyma, message nie zniknie, zostaje zapisana, dopoki nie zostanie przetworzona
                channel.QueueDeclare(queue: _eventBusSettings.EventQueue, true, false);

                var message = new CreateCandidateApplicationEvent()
                {
                    OfferId = request.OfferId,
                    UserId = request.UserId
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                //:)
                // Note on Message Persistence
                // Marking messages as persistent doesn't fully guarantee that a message won't be lost.
                // Although it tells RabbitMQ to save the message to disk, there is still a short time
                // window when RabbitMQ has accepted a message and hasn't saved it yet. Also,
                // RabbitMQ doesn't do fsync(2) for every message -- it may be just saved to cache and not really written to the disk.
                // The persistence guarantees aren't strong, but it's more than enough for our simple task queue.
                // If you need a stronger guarantee then you can use publisher confirms.
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //"" - default exchange
                channel.BasicPublish(exchange: "",
                    routingKey: _eventBusSettings.EventQueue,
                    basicProperties: properties,
                    body: body);
                
                _logger.LogInformation("CreatedCandidateOfferCommandHandler > Handle - end");
            }

        }
        catch (Exception e)
        {
            _logger.LogWarning("CreatedCandidateOfferCommandHandler > Handle - sending msg to rabbitmq failure");
          
        }


        return await Task.FromResult(new CreatedCandidateOfferCommandResponse(){UserOfferId = Guid.NewGuid()});
    }
}                    