using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Categories;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Categories;

public class DeleteCategoryCommandHandler : CommandHandlerBase, ICommandHandler<DeleteCategoryCommand>
{
    private readonly IValidator<DeleteCategoryCommand> _commandValidaor;
    private readonly IRepository<CategoryEntity> _repository;

    public DeleteCategoryCommandHandler(
        IValidator<DeleteCategoryCommand> commandValidaor,
        IRepository<CategoryEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(DeleteCategoryCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            await _repository.DeleteByIdAsync(ObjectIdMapper.ToObjectId(command.Id));
        }

        return Return();
    }
}