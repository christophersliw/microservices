using Microsoft.AspNetCore.Mvc;

namespace mikroserwisy.Controllers
{
	[ApiController]
	[Route("api/basket/basket")]
	public class BasketController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Post(string userName)
		{
			return Ok("ok");
		}
	}
}