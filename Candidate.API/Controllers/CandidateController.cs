using Candidate.Application.Functions.Candidates.Commands;
using Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;
using Candidate.Application.Functions.Candidates.Events;
using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.API.Controllers;

[ApiController]
[Route("api/candidateservice/[controller]")]
public class CandidateController : ControllerBase
{
    private readonly ILogger<CandidateController> _logger;
    private readonly IMediator _mediator;
    
    public CandidateController(ILogger<CandidateController> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    //GET: api/candidateservice/candidate
    [HttpGet]
    public async Task<ActionResult<List<UserViewModel>>> Get(int pageSize = 10, int pageIndex = 0)
    {
        _logger.LogInformation("start - CandidateController > Get");
        
        var userInListViewModel = await _mediator.Send(new GetUserListQuery() {PageIndex = pageIndex, PageSize = pageSize});

        _logger.LogInformation("end - CandidateController > Get");
        return Ok(userInListViewModel);
    }

    //POST: api/candidateservice/candidate
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatedCandidateOfferCommand createdCandidateOfferCommand)
    {
        _logger.LogInformation("start - CandidateController > Post");
        
        await _mediator.Send(createdCandidateOfferCommand);

        return Ok();
    }
    
    //PUT: api/candidateservice/candidate/{id:int}/changestatus
    [HttpPut("{id:guid}/changestatus")]
    public async Task<ActionResult> ChangeStatus([FromQuery] Guid id, [FromBody] UpdateCandidateAplicationStatusCommand updateCandidateAplicationStatusCommand)
    {
        await _mediator.Send(updateCandidateAplicationStatusCommand);

        return Ok();
    }
    
    

    
}