using AutoMapper;
using Candidate.API.Client;
using Candidate.API.Client.RequestContent;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Event.Application.Functions.Candidate.Events;

public class CreateCandidateApplicationEventHandler : IRequestHandler<CreateCandidateApplicationEvent>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCandidateApplicationEventHandler> _logger;
    private readonly ICandidateClient _candidateClient; 

    public CreateCandidateApplicationEventHandler(
        IMapper mapper, 
        ILogger<CreateCandidateApplicationEventHandler> logger, 
        ICandidateClient candidateClient)
    {
        _mapper = mapper;
        _logger = logger;
        _candidateClient = candidateClient;
    }
    
    public async Task<Unit> Handle(CreateCandidateApplicationEvent request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCandidateApplicationEventHandler > Handle");

        await _candidateClient.UpdateApplicationStatusResource.UpdateStatus(new UpdateStatusRequest()
        {
            StatusId = 2,
            UserOfferId = request.UserOfferId,
        }, cancellationToken);


        return Unit.Value;
    }
}