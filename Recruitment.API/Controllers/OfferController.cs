using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Application.Functions;

namespace Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitmentseservice/[controller]")]
public class OfferController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<OfferController> _logger;

    public OfferController(IMediator mediator, ILogger<OfferController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    //GET: api/recruitmentseservice/offer/3
    [HttpGet("{id}")]
    public async Task<ActionResult<OfferViewModel>> Get(int id)
    {
        _logger.LogInformation("start - OfferController > Get");
        
        var offer = await _mediator.Send(new GetOfferByIdQuery() {OfferId = id});

        return Ok(offer);
    }
}