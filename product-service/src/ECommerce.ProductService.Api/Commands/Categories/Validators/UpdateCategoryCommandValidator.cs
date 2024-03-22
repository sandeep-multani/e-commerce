using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Categories;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Categories.Validators;

public class UpdateCategoryCommandValidator : CategoryCommandValidatorBase<UpdateCategoryCommand>
{
    private readonly IRepository<CategoryEntity> _repository;

    public UpdateCategoryCommandValidator(IRepository<CategoryEntity> repository)
    : base(repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));

        ValidateId();
        ValidateCodeNotChanged();
    }

    private void ValidateId()
    {
        RuleFor(command => command.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Category Id can't be empty");
    }

    private void ValidateCodeNotChanged()
    {
        RuleFor(command => command)
        .MustAsync(async (command, cancellationToken) =>
                await _repository.FindOneAsync(x => x.CategoryCode == command.CategoryCode && x.Id == ObjectIdMapper.ToObjectId(command.Id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Category with id '{x.Id}' and code '{x.CategoryCode}' does not exists. Category code can't be changed.");
    }
}
