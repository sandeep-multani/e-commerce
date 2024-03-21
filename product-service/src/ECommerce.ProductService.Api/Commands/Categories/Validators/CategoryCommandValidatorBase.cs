using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Categories.Validators;

public abstract partial class CategoryCommandValidatorBase<T> : AbstractValidator<T> where T : CategoryCommandBase
{
    private readonly IRepository<CategoryEntity> _repository;

    protected CategoryCommandValidatorBase(IRepository<CategoryEntity> repository)
    {
        _repository = repository;

        ValidateName();
        ValidateCode();
        ValidateNameIsUnique();
        ValidateCodeIsUnique();
    }

    protected void ValidateNameIsUnique()
    {
        RuleFor(command => command.Name)
            .MustAsync(async (name, cancellationToken) => await _repository.FindOneAsync(p => p.Name == name) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A Category with name '{x.Name}' already exists");
    }

    protected void ValidateCodeIsUnique()
    {
        RuleFor(command => command.CategoryCode)
            .MustAsync(async (code, cancellationToken) => await _repository.FindOneAsync(p => p.CategoryCode == code) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A Category with code '{x.CategoryCode}' already exists");
    }

    private void ValidateName()
    {
        RuleFor(command => command.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Category name can't be empty");
    }

    private void ValidateCode()
    {
        RuleFor(command => command.CategoryCode)
            .Must(desc => !string.IsNullOrWhiteSpace(desc))
            .WithSeverity(Severity.Error)
            .WithMessage("Category code can't be empty");
    }
}