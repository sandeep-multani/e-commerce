using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Queries.Brands;
using ECommerce.ProductService.Api.CommandHandlers;
using ECommerce.ProductService.Api.Commands.Brands;

namespace ECommerce.ProductService.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly IBrandQueries _queries;
    private readonly ICommandHandler<CreateBrandCommand> _createBrandCommandHandler;
    private readonly ICommandHandler<UpdateBrandCommand> _updateBrandCommandHandler;
    private readonly ICommandHandler<DeleteBrandCommand> _deleteBrandCommandHandler;

    public BrandController(ILogger<BrandController> logger,
    IBrandQueries queries,
    ICommandHandler<CreateBrandCommand> createBrandCommandHandler,
    ICommandHandler<UpdateBrandCommand> updateBrandCommandHandler,
    ICommandHandler<DeleteBrandCommand> deleteBrandCommandHandler)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _queries = Guard.Against.Null(queries, nameof(queries));
        _createBrandCommandHandler = Guard.Against.Null(createBrandCommandHandler, nameof(createBrandCommandHandler));
        _updateBrandCommandHandler = Guard.Against.Null(updateBrandCommandHandler, nameof(updateBrandCommandHandler));
        _deleteBrandCommandHandler = Guard.Against.Null(deleteBrandCommandHandler, nameof(deleteBrandCommandHandler));
    }

    // GET: api/v1/brand
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBrands()
    {
        _logger.LogInformation("Getting brands");
        return Ok(await _queries.GetAllAsync());
    }

    // GET: api/v1/brand/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBrand(string id)
    {
        _logger.LogInformation("Getting brand by id");
        var brand = await _queries.GetByIdAsync(id);
        if (brand == null)
        {
            return NotFound();
        }

        return Ok(brand);
    }

    // POST: api/v1/brand
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBrand(CreateBrandCommand command)
    {
        _logger.LogInformation("Creating brand");
        var result = await _createBrandCommandHandler.HandleAsync(command);
        if (result.Success)
            return CreatedAtAction(nameof(GetBrand), new { id = result.ResourceId }, command);
        return BadRequest(result.Errors);
    }

    // PUT: api/v1/brand
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBrand(UpdateBrandCommand command)
    {
        _logger.LogInformation("Creating brand");
        var result = await _updateBrandCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }

    // DELETE: api/v1/brand/5
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBrand(DeleteBrandCommand command)
    {
        _logger.LogInformation("Deleting brand");
        var result = await _deleteBrandCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }
}