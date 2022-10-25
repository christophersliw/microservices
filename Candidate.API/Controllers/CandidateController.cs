using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.API.Controllers;

[ApiController]
[Route("api/candidateservice/[controller]")]
public class CandidateController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //GET: api/candidateservice/candidate
    [HttpGet]
    public async Task<ActionResult<List<UserViewModel>>> Get(int pageSize = 10, int pageIndex = 0)
    {
        var userInListViewModel = await _mediator.Send(new GetUserListQuery() {PageIndex = pageIndex, PageSize = pageSize});

        return Ok(userInListViewModel);
    }
}