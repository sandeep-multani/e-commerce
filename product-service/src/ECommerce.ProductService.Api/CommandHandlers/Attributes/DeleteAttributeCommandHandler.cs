using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Attributes;
using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Attributes;

public class DeleteAttributeCommandHandler : CommandHandlerBase, ICommandHandler<DeleteAttributeCommand>
{
    private readonly IValidator<DeleteAttributeCommand> _commandValidaor;
    private readonly IRepository<AttributeEntity> _repository;

    public DeleteAttributeCommandHandler(
        IValidator<DeleteAttributeCommand> commandValidaor,
        IRepository<AttributeEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(DeleteAttributeCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            await _repository.DeleteByIdAsync(ObjectIdMapper.ToObjectId(command.Id));
        }

        return Return();
    }
}