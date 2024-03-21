using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public class CreateProductCommandValidator : ProductCommandValidatorBase<CreateProductCommand>
{
    public CreateProductCommandValidator(IRepository<ProductEntity> repository)
        : base(repository)
    {
    }
}