using Microsoft.AspNetCore.Mvc;
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
    private readonly ICommandHandler<UpdateProductCommand> _updateProductCommandHandler;
    private readonly ICommandHandler<DeleteProductCommand> _deleteProductCommandHandler;

    public ProductController(ILogger<ProductController> logger,
    IProductQueries queries,
    ICommandHandler<CreateProductCommand> createProductCommandHandler,
    ICommandHandler<UpdateProductCommand> updateProductCommandHandler,
    ICommandHandler<DeleteProductCommand> deleteProductCommandHandler)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _queries = Guard.Against.Null(queries, nameof(queries));
        _createProductCommandHandler = Guard.Against.Null(createProductCommandHandler, nameof(createProductCommandHandler));
        _updateProductCommandHandler = Guard.Against.Null(updateProductCommandHandler, nameof(updateProductCommandHandler));
        _deleteProductCommandHandler = Guard.Against.Null(deleteProductCommandHandler, nameof(deleteProductCommandHandler));
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
        _logger.LogInformation("Getting product by id");
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
        var result = await _createProductCommandHandler.HandleAsync(command);
        if (result.Success)
            return CreatedAtAction(nameof(GetProduct), new { id = result.ResourceId }, command);
        return BadRequest(result.Errors);
    }

    // PUT: api/v1/product
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
    {
        _logger.LogInformation("Creating product");
        var result = await _updateProductCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }

    // DELETE: api/v1/product/5
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommand command)
    {
        _logger.LogInformation("Deleting product");
        var result = await _deleteProductCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }
}