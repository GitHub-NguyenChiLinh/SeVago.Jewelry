using DTOs.Requests.Customers;
using DTOs.Requests.Products;
using DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase {
    private readonly IProductService _productService;

    public ProductsController(
        IProductService productService) {
        _productService = productService;
    }
   

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts() {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateProduct([FromBody] CreateProductDto createDto)
    {
        var result = await _productService.CreateProductAsync(createDto);
                    if (result.Success)
            return CreatedAtAction(nameof(GetProducts), new { id = 0 }, result);
        return BadRequest(result);
    }


}
