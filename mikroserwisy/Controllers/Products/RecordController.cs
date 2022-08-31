using Microsoft.AspNetCore.Mvc;

namespace mikroserwisy.Controllers
{
	[ApiController]
	[Route("api/products/record")]
	public class RecordController : ControllerBase
	{
		private readonly HttpClient _httpClient;

		public RecordController()
		{
			_httpClient = new HttpClient();
		}
		
		private IList<string> _productList = new List<string>()
		{
			"prcduct1", "product2", "procuct3"
		};

		[HttpGet]
		public async  Task<IActionResult> Get()
		{
			var response = await _httpClient.GetAsync("http://productapi/api/productservice/record");

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<List<string>>();
			
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