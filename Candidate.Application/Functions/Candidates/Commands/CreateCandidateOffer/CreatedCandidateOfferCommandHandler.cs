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
                channel.QueueDeclare(queue: _eventBusSettings.EventQueue, true, false);

                var message = new CreateCandidateApplicationEvent()
                {
                    OfferId = request.OfferId,
                    UserId = request.UserId
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

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


        return await Task.FromResult(new CreatedCandidateOfferCommandResponse());
    }
}                    