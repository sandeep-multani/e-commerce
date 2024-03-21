using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Brands.Validators;

public abstract partial class BrandCommandValidatorBase<T> : AbstractValidator<T> where T : BrandCommandBase
{
    private readonly IRepository<BrandEntity> _repository;

    protected BrandCommandValidatorBase(IRepository<BrandEntity> repository)
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
            .WithMessage(x => $"A Brand with name '{x.Name}' already exists");
    }

    protected void ValidateCodeIsUnique()
    {
        RuleFor(command => command.BrandCode)
            .MustAsync(async (code, cancellationToken) => await _repository.FindOneAsync(p => p.BrandCode == code) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A Brand with code '{x.BrandCode}' already exists");
    }

    private void ValidateName()
    {
        RuleFor(command => command.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Brand name can't be empty");
    }

    private void ValidateCode()
    {
        RuleFor(command => command.BrandCode)
            .Must(desc => !string.IsNullOrWhiteSpace(desc))
            .WithSeverity(Severity.Error)
            .WithMessage("Brand code can't be empty");
    }
}