using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Categories;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Categories;

public class CreateCategoryCommandHandler : CommandHandlerBase, ICommandHandler<CreateCategoryCommand>
{
    private readonly IValidator<CreateCategoryCommand> _commandValidaor;
    private readonly IRepository<CategoryEntity> _repository;

    public CreateCategoryCommandHandler(
        IValidator<CreateCategoryCommand> commandValidaor,
        IRepository<CategoryEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(CreateCategoryCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var Category = CategoryMapper.CommandToEntity(command);
            await _repository.InsertOneAsync(Category);
            return Return(Category.Id.ToString());
        }

        return Return();
    }
}