using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Application.Functions;

namespace Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitmentseservice/[controller]")]
public class OfferController : Controller
{
    private readonly IMediator _mediator;
    
    public OfferController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //GET: api/recruitmentseservice/offer/3
    [HttpGet("{id}")]
    public async Task<ActionResult<OfferViewModel>> Get(int id)
    {
        var offer = await _mediator.Send(new GetOfferByIdQuery() {OfferId = id});

        return Ok(offer);
    }
}