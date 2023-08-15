using Gateway.Application.Clients;
using Gateway.Application.Responses.Candidate.Candidate;
using Gateway.Application.Responses.Candidate.Candidate.Request;
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
        
        string path = "api/v1/candidateservice/candidates/search";
        string query = $"?pageSize={pageSize}&pageIndex={pageIndex}";
        
        var result = await _candidateClient.GetAsync<List<UserInListViewModelResponse>>(path, query,cancellationToken);
        
        _logger.LogInformation("End - CandidateController > Get");
			
        return Ok(result);
    }
    
    //POST: api/gateway/candidateservice/candidate
    [HttpPost]
    public async Task<ActionResult> Post(CreateCandidateOfferRequest createCandidateOfferRequest, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start - CandidateController > Post");
        
        string path = $"api/candidateservice/candidate";
        
         await _candidateClient.PostAsync<CreateCandidateOfferRequest>(path,null,createCandidateOfferRequest, cancellationToken);
        
        _logger.LogInformation("End - CandidateController > Post");
			
        return Ok();
    }
}