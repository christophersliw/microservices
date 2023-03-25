using AutoMapper;
using Candidate.API.Contracts.V1;
using Candidate.API.Contracts.V1.Requests;
using Candidate.API.Contracts.V1.Responses;
using Candidate.Application.Functions.Candidates.Commands;
using Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;
using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.API.Controllers.V1;

[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly ILogger<CandidatesController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CandidatesController(ILogger<CandidatesController> logger,IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet(ApiRoutes.Candidates.Search)]
    public async Task<ActionResult<List<UserResponse>>> Search(int pageSize = 10,  int pageIndex = 0)
    {
        _logger.LogInformation("start - CandidateController > Search");
        
        var userInListViewModel = await _mediator.Send(new GetUserListQuery() {PageIndex = pageIndex, PageSize = pageSize});

        _logger.LogInformation("end - CandidateController > Search");
        
        return Ok(userInListViewModel);
    }
    
    [HttpGet(ApiRoutes.Candidates.Get)]
    public async Task<ActionResult<List<UserQueryResponse>>> Get([FromQuery]Guid userOfferId)
    {
        _logger.LogInformation("start - CandidateController > Get");
        
     

        _logger.LogInformation("end - CandidateController > Get");
        
        return Ok();
    }
    
    [HttpGet(ApiRoutes.Candidates.GetAll)]
    public async Task<ActionResult<List<UserQueryResponse>>> GetAll()
    {
        _logger.LogInformation("start - CandidateController > GetAll");
        
        var userInListViewModel = await _mediator.Send(new GetUserListQuery() {PageIndex = 0, PageSize = 0});

        _logger.LogInformation("end - CandidateController > GetAll");
        
        return Ok(userInListViewModel);
    }
    
    [HttpPost(ApiRoutes.Candidates.Create)]
    public async Task<ActionResult> Create([FromBody] CreateCandidateApplicationRequest createCandidateApplicationRequest)
    {
        _logger.LogInformation("start - CandidateController > Create");

        var createdCandidateOfferCommand = _mapper.Map<CreatedCandidateOfferCommand>(createCandidateApplicationRequest);
        
        var createdCandidateOfferCommandResponse = await _mediator.Send(createdCandidateOfferCommand);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = $"{baseUrl}/{ApiRoutes.Candidates.Get.Replace("{userOfferId}", createdCandidateOfferCommandResponse.UserOfferId.ToString())}";

        var response = new CreateCandidateApplicationResponse()
            {UserOfferId = createdCandidateOfferCommandResponse.UserOfferId};
        
        return Created(locationUri, response);
    }

    [HttpPatch(ApiRoutes.Candidates.ChangeStatus)]
    public async Task<ActionResult> ChangeStatus([FromQuery] Guid id, [FromBody] UpdateCandidateAplicationStatusCommand updateCandidateAplicationStatusCommand)
    {
        await _mediator.Send(updateCandidateAplicationStatusCommand);

        return Ok();
    }
    
    

    
}