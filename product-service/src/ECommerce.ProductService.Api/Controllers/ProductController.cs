using Microsoft.AspNetCore.Mvc;
using ECommerce.ProductService.Api.Models;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Queries.Products;
using ECommerce.ProductService.Api.CommandHandlers;
using ECommerce.ProductService.Api.Commands.Products;

namespace ECommerce.ProductService.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductQueries _queries;
    private readonly ICommandHandler<CreateProductCommand> _createProductCommandHandler;

    public ProductController(ILogger<ProductController> logger,
    IProductQueries queries,
    ICommandHandler<CreateProductCommand> createProductCommandHandler)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _queries = Guard.Against.Null(queries, nameof(queries));
        _createProductCommandHandler = Guard.Against.Null(createProductCommandHandler, nameof(createProductCommandHandler));
    }

    // GET: api/v1/product
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProducts()
    {
        _logger.LogInformation("Getting products");
        return Ok(await _queries.GetAllAsync());
    }

    // GET: api/v1/product/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _queries.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST: api/v1/product
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        _logger.LogInformation("Creating product");
        var result = await _createProductCommandHandler.Handle(command);
        if (result.Success)
            return Created();
        return BadRequest(result.Errors);
    }
}