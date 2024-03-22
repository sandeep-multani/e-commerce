using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Queries.Attributes;
using ECommerce.ProductService.Api.CommandHandlers;
using ECommerce.ProductService.Api.Commands.Attributes;

namespace ECommerce.ProductService.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AttributeController : ControllerBase
{
    private readonly ILogger<AttributeController> _logger;
    private readonly IAttributeQueries _queries;
    private readonly ICommandHandler<CreateAttributeCommand> _createAttributeCommandHandler;
    private readonly ICommandHandler<UpdateAttributeCommand> _updateAttributeCommandHandler;
    private readonly ICommandHandler<DeleteAttributeCommand> _deleteAttributeCommandHandler;

    public AttributeController(ILogger<AttributeController> logger,
    IAttributeQueries queries,
    ICommandHandler<CreateAttributeCommand> createAttributeCommandHandler,
    ICommandHandler<UpdateAttributeCommand> updateAttributeCommandHandler,
    ICommandHandler<DeleteAttributeCommand> deleteAttributeCommandHandler)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _queries = Guard.Against.Null(queries, nameof(queries));
        _createAttributeCommandHandler = Guard.Against.Null(createAttributeCommandHandler, nameof(createAttributeCommandHandler));
        _updateAttributeCommandHandler = Guard.Against.Null(updateAttributeCommandHandler, nameof(updateAttributeCommandHandler));
        _deleteAttributeCommandHandler = Guard.Against.Null(deleteAttributeCommandHandler, nameof(deleteAttributeCommandHandler));
    }

    // GET: api/v1/attribute
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAttributes()
    {
        _logger.LogInformation("Getting attributes");
        return Ok(await _queries.GetAllAsync());
    }

    // GET: api/v1/attribute/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAttribute(string id)
    {
        _logger.LogInformation("Getting attribute by id");
        var attribute = await _queries.GetByIdAsync(id);
        if (attribute == null)
        {
            return NotFound();
        }

        return Ok(attribute);
    }

    // POST: api/v1/attribute
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAttribute(CreateAttributeCommand command)
    {
        _logger.LogInformation("Creating attribute");
        var result = await _createAttributeCommandHandler.HandleAsync(command);
        if (result.Success)
            return CreatedAtAction(nameof(GetAttribute), new { id = result.ResourceId }, command);
        return BadRequest(result.Errors);
    }

    // PUT: api/v1/attribute
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAttribute(UpdateAttributeCommand command)
    {
        _logger.LogInformation("Updating attribute");
        var result = await _updateAttributeCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }

    // DELETE: api/v1/Attribute/5
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAttribute(DeleteAttributeCommand command)
    {
        _logger.LogInformation("Deleting attribute");
        var result = await _deleteAttributeCommandHandler.HandleAsync(command);
        if (result.Success)
            return Ok();
        return BadRequest(result.Errors);
    }
}