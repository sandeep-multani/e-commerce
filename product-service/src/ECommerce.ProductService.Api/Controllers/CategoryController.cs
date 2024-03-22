using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Queries.Categories;
using ECommerce.ProductService.Api.CommandHandlers;
using ECommerce.ProductService.Api.Commands.Categories;

namespace ECommerce.ProductService.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryQueries _queries;
    private readonly ICommandHandler<CreateCategoryCommand> _createCategoryCommandHandler;
    private readonly ICommandHandler<UpdateCategoryCommand> _updateCategoryCommandHandler;
    private readonly ICommandHandler<DeleteCategoryCommand> _deleteCategoryCommandHandler;

    public CategoryController(ILogger<CategoryController> logger,
    ICategoryQueries queries,
    ICommandHandler<CreateCategoryCommand> createCategoryCommandHandler,
    ICommandHandler<UpdateCategoryCommand> updateCategoryCommandHandler,
    ICommandHandler<DeleteCategoryCommand> deleteCategoryCommandHandler)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _queries = Guard.Against.Null(queries, nameof(queries));
        _createCategoryCommandHandler = Guard.Against.Null(createCategoryCommandHandler, nameof(createCategoryCommandHandler));
        _updateCategoryCommandHandler = Guard.Against.Null(updateCategoryCommandHandler, nameof(updateCategoryCommandHandler));
        _deleteCategoryCommandHandler = Guard.Against.Null(deleteCategoryCommandHandler, nameof(deleteCategoryCommandHandler));
    }

    // GET: api/v1/category
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories()
    {
        _logger.LogInformation("Getting categories");
        return Ok(await _queries.GetAllAsync());
    }

    // GET: api/v1/category/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategory(string id)
    {
        _logger.LogInformation("Getting category by id");
        var category = await _queries.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    // POST: api/v1/category
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
    {
        _logger.LogInformation("Creating category");
        var result = await _createCategoryCommandHandler.HandleAsync(command);
        if (result.Success)
            return CreatedAtAction(nameof(GetCategory), new { id = result.ResourceId }, command);
        return BadRequest(result.Errors);
    }

    // PUT: api/v1/category
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        _logger.LogInformation("Updating category");
        var result = await _updateCategoryCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }

    // DELETE: api/v1/category/5
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand command)
    {
        _logger.LogInformation("Deleting category");
        var result = await _deleteCategoryCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }
}