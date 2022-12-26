using Candidate.Application.Configurations;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Candidate.Application.Functions.Candidates.Commands;

public class UpdateCandidateAplicationStatusHandler : IRequestHandler<UpdateCandidateAplicationStatusCommand, UpdateCandidateAplicationStatusResponse>
{
    private readonly ILogger<UpdateCandidateAplicationStatusHandler> _logger;
    private readonly ConnectionFactory _connectionFactory;
    private readonly EventBusSettings _eventBusSettings;

    public UpdateCandidateAplicationStatusHandler(ILogger<UpdateCandidateAplicationStatusHandler> logger, ConnectionFactory connectionFactory, EventBusSettings eventBusSettings)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
        _eventBusSettings = eventBusSettings;
    }
    public async Task<UpdateCandidateAplicationStatusResponse> Handle(UpdateCandidateAplicationStatusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ChangeCandidateAplicationStatusHandler > Handle - start");
        
        
        
        _logger.LogInformation("ChangeCandidateAplicationStatusHandler > Handle - end");

        return await Task.FromResult(new UpdateCandidateAplicationStatusResponse());
    }
}                    