using ECommerce.ProductService.Api.Entities.Products;
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
        ValidateNameIsUnique();
        ValidateSkuIsUnique();
    }

    protected void ValidateNameIsUnique()
    {
        RuleFor(productBaseCommand => productBaseCommand.Name)
            .MustAsync(async (name, cancellationToken) => await _repository.FindOneAsync(p => p.Name == name) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A product with name '{x.Name}' already exists");
    }

    protected void ValidateSkuIsUnique()
    {
        RuleFor(productBaseCommand => productBaseCommand.Sku)
            .MustAsync(async (sku, cancellationToken) => await _repository.FindOneAsync(p => p.Sku == sku) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A product with SKU '{x.Sku}' already exists");
    }

    private void ValidateName()
    {
        RuleFor(productBaseCommand => productBaseCommand.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Product name can't be empty");
    }

    private void ValidateDescription()
    {
        RuleFor(productBaseCommand => productBaseCommand.Description)
            .Must(desc => !string.IsNullOrWhiteSpace(desc))
            .WithSeverity(Severity.Error)
            .WithMessage("Product description can't be empty");
    }

    private void ValidateSku()
    {
        RuleFor(productBaseCommand => productBaseCommand.Sku)
            .Must(sku => !string.IsNullOrWhiteSpace(sku))
            .WithSeverity(Severity.Error)
            .WithMessage("Product SKU can't be empty");
    }
}