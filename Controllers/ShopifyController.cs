using Microsoft.AspNetCore.Mvc;
using ShopifySharp;

namespace ShopifyIntegrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopifyController : ControllerBase
    {
        //private readonly IShopifyService _service;
private readonly IGraphService _graphService;
private readonly IProductService _productService;

public ShopifyController(IGraphService graphService, IProductService productService)
{
    //_service = service;
    _graphService = graphService;
    _productService = productService;
}

[HttpGet("products")]
// public async Task<ActionResult<Product>> GetProducts(long id, CancellationToken cancelationToken)
// {
//     //var products = await _service.GetProductsAsync();
//     //var products = await _productService.GetAsync(id);
//     //var products = await _graphService.;
//     //var products = await _graphService.
//     //return Ok(products);
// }

[HttpPost]
public async Task<ActionResult<Product>> CreateProduct(CancellationToken cancellationToken)
{
    const string query = @"
            mutation {
              productCreate(input: {title: ""Produto de teste"", tags: ""teste""}) {
                product {
                  id
                  title
                }
              }
            }";
    var createdProduct = await _graphService.PostAsync(query);
    return Ok(createdProduct);
}
    }
}