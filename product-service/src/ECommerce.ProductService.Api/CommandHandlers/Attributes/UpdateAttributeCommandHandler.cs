using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Attributes;
using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Attributes;

public class UpdateAttributeCommandHandler : CommandHandlerBase, ICommandHandler<UpdateAttributeCommand>
{
    private readonly IValidator<UpdateAttributeCommand> _commandValidaor;
    private readonly IRepository<AttributeEntity> _repository;

    public UpdateAttributeCommandHandler(
        IValidator<UpdateAttributeCommand> commandValidaor,
        IRepository<AttributeEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(UpdateAttributeCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var attribute = AttributeMapper.CommandToEntity(command);
            await _repository.ReplaceOneAsync(attribute);
        }

        return Return();
    }
}