using Authentication.API.Contracts.V1;
using Authentication.API.Contracts.V1.Requests;
using Authentication.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers.V1;

public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost(ApiRoutes.Identity.Register)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
    {
        
        return Ok();
    }
}