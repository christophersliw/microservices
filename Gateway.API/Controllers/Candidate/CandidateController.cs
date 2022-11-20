using Gateway.Application.Clients;
using Gateway.Application.Responses.Candidate.Candidate;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers.Candidate;

[ApiController]
[Route("api/candidateservice/candidate")]
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
        // var response = await _httpClient.GetAsync($"https://localhost:7130/api/candidateservice/candidate?pageSize={pageSize}&pageIndex={pageIndex}");
        //
        // response.EnsureSuccessStatusCode();
        //
        // var result = await response.Content.ReadFromJsonAsync<List<UserInListViewModelResponse>>();

        _logger.LogInformation("Start - CandidateController > Get");
        
        string path = $"https://localhost:7130/api/candidateservice/candidate?pageSize={pageSize}&pageIndex={pageIndex}";
        
        var result = await _candidateClient.GetAsync<List<UserInListViewModelResponse>>(path, cancellationToken);
        
        _logger.LogInformation("End - CandidateController > Get");
			
        return Ok(result);
    }
}