using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public abstract partial class ProductCommandValidatorBase<T> : AbstractValidator<T> where T : ProductCommandBase
{
    private readonly IRepository<ProductEntity> _repository;

    protected ProductCommandValidatorBase(IRepository<ProductEntity> repository)
    {
        _repository = repository;

        ValidateName();
        ValidateDescription();
        ValidateSku();
    }

    protected void ValidateNameIsUniqueOnCreate()
    {
        RuleFor(productBaseCommand => productBaseCommand.Name)
            .MustAsync(async (name, cancellationToken) => await _repository.FindOneAsync(f => f.Name == name) is not null)
            .WithSeverity(Severity.Error)
            .WithMessage("A product with this name already exists.");
    }

    private void ValidateName()
    {
        RuleFor(productBaseCommand => productBaseCommand.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithSeverity(Severity.Error)
            .WithMessage("Product name can't be empty");
    }

    private void ValidateDescription()
    {
        RuleFor(productBaseCommand => productBaseCommand.Description)
            .Must(desc => !Guid.Empty.Equals(desc))
            .WithSeverity(Severity.Error)
            .WithMessage("Product description can't be empty");
    }

    private void ValidateSku()
    {
        RuleFor(productBaseCommand => productBaseCommand.Sku)
            .Must(sku => !Guid.Empty.Equals(sku))
            .WithSeverity(Severity.Error)
            .WithMessage("Product SKU can't be empty");
    }
}