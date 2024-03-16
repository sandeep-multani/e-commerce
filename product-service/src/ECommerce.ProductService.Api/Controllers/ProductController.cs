using Microsoft.AspNetCore.Mvc;
using ECommerce.ProductService.Api.Services;
using ECommerce.ProductService.Api.Models;
using Ardalis.GuardClauses;

namespace ECommerce.ProductService.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _productService = Guard.Against.Null(productService, nameof(productService));
    }

    // GET: api/v1/product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        _logger.LogInformation("Getting products");
        return await _productService.GetProductsAsync();
    }

    // GET: api/v1/product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _productService.GetProductAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }
}