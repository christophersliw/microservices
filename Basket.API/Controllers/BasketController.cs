using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
	[ApiController]
	[Route("api/basketservice/basket")]
	public class BasketController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Post(string userName)
		{
			return Ok("ok");
		}
	}
}