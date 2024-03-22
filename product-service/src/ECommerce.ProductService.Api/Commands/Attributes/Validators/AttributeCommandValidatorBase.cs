using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Attributes.Validators;

public abstract partial class AttributeCommandValidatorBase<T> : AbstractValidator<T> where T : AttributeCommandBase
{
    private readonly IRepository<AttributeEntity> _repository;

    protected AttributeCommandValidatorBase(IRepository<AttributeEntity> repository)
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
            .WithMessage(x => $"A Attribute with name '{x.Name}' already exists");
    }

    protected void ValidateCodeIsUnique()
    {
        RuleFor(command => command.AttributeCode)
            .MustAsync(async (code, cancellationToken) => await _repository.FindOneAsync(p => p.AttributeCode == code) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A Attribute with code '{x.AttributeCode}' already exists");
    }

    private void ValidateName()
    {
        RuleFor(command => command.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Attribute name can't be empty");
    }

    private void ValidateCode()
    {
        RuleFor(command => command.AttributeCode)
            .Must(desc => !string.IsNullOrWhiteSpace(desc))
            .WithSeverity(Severity.Error)
            .WithMessage("Attribute code can't be empty");
    }
}