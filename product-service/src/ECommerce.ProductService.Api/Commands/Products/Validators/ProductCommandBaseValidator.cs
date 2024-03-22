using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Entities.Categories;
using ECommerce.ProductService.Api.Entities.Products;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public abstract partial class ProductCommandValidatorBase<T> : AbstractValidator<T> where T : ProductCommandBase
{
    private readonly IRepository<ProductEntity> _productRepository;
    private readonly IRepository<AttributeEntity> _attributeRepository;
    private readonly IRepository<BrandEntity> _brandRepository;
    private readonly IRepository<CategoryEntity> _categoryRepository;

    protected ProductCommandValidatorBase(
        IRepository<ProductEntity> productRepository,
        IRepository<AttributeEntity> attributeRepository,
        IRepository<BrandEntity> brandRepository,
        IRepository<CategoryEntity> categoryRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
        _attributeRepository = Guard.Against.Null(attributeRepository, nameof(attributeRepository));
        _brandRepository = Guard.Against.Null(brandRepository, nameof(brandRepository));
        _categoryRepository = Guard.Against.Null(categoryRepository, nameof(categoryRepository));

        ValidateName();
        ValidateDescription();
        ValidateSku();
        ValidateBrandCode();
        ValidateCategoryCode();
        ValidateAttributes();

        ValidateNameIsUnique();
        ValidateSkuIsUnique();
        ValidateBrandExists();
        ValidateCategoryExists();
    }

    private void ValidateNameIsUnique()
    {
        RuleFor(command => command.Name)
            .MustAsync(async (name, cancellationToken) => await _productRepository.FindOneAsync(p => p.Name == name) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A product with name '{x.Name}' already exists");
    }

    private void ValidateSkuIsUnique()
    {
        RuleFor(command => command.Sku)
            .MustAsync(async (sku, cancellationToken) => await _productRepository.FindOneAsync(p => p.Sku == sku) is null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"A product with SKU '{x.Sku}' already exists");
    }

    private void ValidateName()
    {
        RuleFor(command => command.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Product name can't be empty");
    }

    private void ValidateDescription()
    {
        RuleFor(command => command.Description)
            .Must(desc => !string.IsNullOrWhiteSpace(desc))
            .WithSeverity(Severity.Error)
            .WithMessage("Product description can't be empty");
    }

    private void ValidateSku()
    {
        RuleFor(command => command.Sku)
            .Must(sku => !string.IsNullOrWhiteSpace(sku))
            .WithSeverity(Severity.Error)
            .WithMessage("Product SKU can't be empty");
    }

    private void ValidateBrandCode()
    {
        RuleFor(command => command.BrandCode)
            .Must(brandCode => !string.IsNullOrWhiteSpace(brandCode))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Brand code can't be empty");
    }

    private void ValidateCategoryCode()
    {
        RuleFor(command => command.CategoryCode)
            .Must(categoryCode => !string.IsNullOrWhiteSpace(categoryCode))
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Category code can't be empty");
    }

    private void ValidateBrandExists()
    {
        RuleFor(command => command.BrandCode)
            .MustAsync(async (brandCode, cancellationToken) =>
                await _brandRepository.FindOneAsync(b => b.BrandCode == brandCode) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Brand with code '{x.BrandCode}' does not exists");
    }

    private void ValidateCategoryExists()
    {
        RuleFor(command => command.CategoryCode)
            .MustAsync(async (categoryCode, cancellationToken) =>
                await _categoryRepository.FindOneAsync(b => b.CategoryCode == categoryCode) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Category with code '{x.CategoryCode}' does not exists");
    }

    private void ValidateAttributes()
    {
        RuleForEach(command => command.Attributes).SetValidator(new ProductAttributeValidator(_attributeRepository));
    }
}