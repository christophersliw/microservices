using Microsoft.AspNetCore.Mvc;

namespace mikroserwisy.Controllers
{
	[ApiController]
	[Route("api/products/record")]
	public class RecordController : ControllerBase
	{
		private IList<string> _productList = new List<string>()
		{
			"prcduct1", "product2", "procuct3"
		};

		[HttpGet]
		public async  Task<IActionResult> Get()
		{
			 var result =  await  GetProductList();

			return Ok(result);
		}
		

		private async Task<IList<string>> GetProductList()
		{
			await Task.Run(() =>
			{
			});
        
			return _productList;
		}
	}
}