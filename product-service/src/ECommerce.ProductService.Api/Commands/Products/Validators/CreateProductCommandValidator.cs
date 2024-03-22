using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Entities.Categories;
using ECommerce.ProductService.Api.Entities.Products;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public class CreateProductCommandValidator : ProductCommandValidatorBase<CreateProductCommand>
{
    public CreateProductCommandValidator(
        IRepository<ProductEntity> productRepository,
        IRepository<AttributeEntity> attributeRepository,
        IRepository<BrandEntity> brandRepository,
        IRepository<CategoryEntity> categoryRepository
    ) : base(productRepository, attributeRepository, brandRepository, categoryRepository)
    {
    }
}