using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Attributes;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Attributes;

public class CreateAttributeCommandHandler : CommandHandlerBase, ICommandHandler<CreateAttributeCommand>
{
    private readonly IValidator<CreateAttributeCommand> _commandValidaor;
    private readonly IRepository<AttributeEntity> _repository;

    public CreateAttributeCommandHandler(
        IValidator<CreateAttributeCommand> commandValidaor,
        IRepository<AttributeEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(CreateAttributeCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var attribute = AttributeMapper.CommandToEntity(command);
            await _repository.InsertOneAsync(attribute);
            return Return(attribute.Id.ToString());
        }

        return Return();
    }
}