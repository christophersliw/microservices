using Gateway.Application.Clients;
using Gateway.Application.Responses.Candidate.Candidate;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers.Candidate;

[ApiController]
[Route("api/gateway/candidateservice/candidate")]
public class CandidateController : ControllerBase
{
    private readonly ILogger<CandidateController> _logger;
    private readonly ICandidateClient _candidateClient;

    public CandidateController(ILogger<CandidateController> logger, ICandidateClient candidateClient)
    {
        _logger = logger;
        _candidateClient = candidateClient;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserInListViewModelResponse>>> Get(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
    {
        _logger.LogInformation("Start - CandidateController > Get");
        
        string path = $"api/candidateservice/candidate?pageSize={pageSize}&pageIndex={pageIndex}";
        
        var result = await _candidateClient.GetAsync<List<UserInListViewModelResponse>>(path, cancellationToken);
        
        _logger.LogInformation("End - CandidateController > Get");
			
        return Ok(result);
    }
}