using KnowHow.Application.Functions.Commands.OneEventManyConsumer;
using KnowHow.Application.Functions.Commands.OneEventOneConsumer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnowHow.API.Controllers;

[ApiController]
[Route("api/knowhowservice/[controller]")]
public class KnowHowController : Controller
{
    private IMediator _mediator;
    private ILogger<KnowHowController> _logger;
    
    public KnowHowController(ILogger<KnowHowController> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpPost("oneeventoneconsumer")]
    public async Task<ActionResult> OneEventOneConsumer()
    {
        await _mediator.Send(new ExecuteOneEventOneConsumerCommand());
        
        return Ok();
    }

    [HttpPost("oneeventmanyconsumer")]
    public async Task<ActionResult> OneEventManyConsumer()
    {
        await _mediator.Send(new ExecuteOneEventManyConsumerCommand());
        
        return Ok();
    }
}