using Gateway.Application.Responses.Candidate.Candidate;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers.Candidate;

[ApiController]
[Route("api/candidateservice/candidate")]
public class CandidateController : ControllerBase
{
    private readonly HttpClient _httpClient;
    
    public CandidateController()
    {
        _httpClient = new HttpClient();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserInListViewModelResponse>>> Get(int pageSize = 10, int pageIndex = 0)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7130/api/candidateservice/candidate?pageSize={pageSize}&pageIndex={pageIndex}");

        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<List<UserInListViewModelResponse>>();
			
        return Ok(result);
    }
}