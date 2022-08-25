using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers;

 [ApiController]
 [Route("api/productservice/record")]
public class RecordController : ControllerBase
{
    private IList<string> _productList = new List<string>()
    {
        "prcduct1", "product2", "procuct3"
    };

    [HttpGet]
    public async Task<ActionResult> Get(int pageSize = 10, int pageIndex = 0)
    {
        var result = await  GetProductList();

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