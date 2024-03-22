using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Entities.Categories;
using ECommerce.ProductService.Api.Entities.Products;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public class UpdateProductCommandValidator : ProductCommandValidatorBase<UpdateProductCommand>
{
    public UpdateProductCommandValidator(
        IRepository<ProductEntity> productRepository,
        IRepository<AttributeEntity> attributeRepository,
        IRepository<BrandEntity> brandRepository,
        IRepository<CategoryEntity> categoryRepository
    ) : base(productRepository, attributeRepository, brandRepository, categoryRepository)
    {
        ValidateId();
    }

    private void ValidateId()
    {
        RuleFor(updateProductCommand => updateProductCommand.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Product Id can't be empty");
    }
}
