using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Categories;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Categories;

public class UpdateCategoryCommandHandler : CommandHandlerBase, ICommandHandler<UpdateCategoryCommand>
{
    private readonly IValidator<UpdateCategoryCommand> _commandValidaor;
    private readonly IRepository<CategoryEntity> _repository;

    public UpdateCategoryCommandHandler(
        IValidator<UpdateCategoryCommand> commandValidaor,
        IRepository<CategoryEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(UpdateCategoryCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var category = CategoryMapper.CommandToEntity(command);
            await _repository.ReplaceOneAsync(category);
        }

        return Return();
    }
}