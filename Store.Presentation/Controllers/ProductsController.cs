using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Exception;
using Store.Service.Abstractions;
using Store.Shared.DTOs;
using Store.Shared.DTOs.ErrorModels;
using Store.Shared.DTOs.ProductDto;
namespace Store.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IServiceManager _serviceManager) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PaginationResponse<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorDetails))]
    public async Task<ActionResult<ProductResponse>> GetProducts([FromQuery] ProductQueryParams productQuery)
    {
        var products = await _serviceManager.ProductService.GetAllProductAsync(productQuery);
        return products is not null ? Ok(products): BadRequest() ;
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetProductById(int? Id)
    {
        if (Id == null) BadRequest();

        var product = await _serviceManager.ProductService.GetProductById(Id.Value);

        if (product != null) return Ok(product);
        
        throw new NotFoundException(Id.Value);

    }

    [HttpGet("Brands")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<BrandTypeResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllBrands()
    {
        var res = await _serviceManager.ProductService.GetAllBrandAsync();
        if (res != null) return Ok(res);
        return BadRequest();
    }

    [HttpGet("Types")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<BrandTypeResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllTypes()
    {
        var res = await _serviceManager.ProductService.GetAllTypeAsync();
        if (res != null) return Ok(res);
        return BadRequest();
    }
}