using Microsoft.AspNetCore.Mvc;
using Store.Service.Abstractions;
using Store.Shared.DTOs.ProductDto;
namespace Store.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IServiceManager _serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParams productQuery)
    {
        var products = await _serviceManager.ProductService.GetAllProductAsync(productQuery);
        return products is null ? BadRequest() : Ok(products);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetProductById(int? Id)
    {
        if (Id == null) return BadRequest("Product ID is required");

        var product = await _serviceManager.ProductService.GetProductById(Id.Value);

        if (product != null) return Ok(product);

        return NotFound($"Product with ID {Id} not found");
    }

    [HttpGet("Brands")]
    public async Task<IActionResult> GetAllBrands()
    {
        var res = await _serviceManager.ProductService.GetAllBrandAsync();
        if (res != null) return Ok(res);
        return BadRequest();
    }

    [HttpGet("Types")]
    public async Task<IActionResult> GetAllTypes()
    {
        var res = await _serviceManager.ProductService.GetAllTypeAsync();
        if (res != null) return Ok(res);
        return BadRequest();
    }
}