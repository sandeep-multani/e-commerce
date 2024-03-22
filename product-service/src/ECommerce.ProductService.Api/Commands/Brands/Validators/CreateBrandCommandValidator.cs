using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Commands.Brands.Validators;

public class CreateBrandCommandValidator : BrandCommandValidatorBase<CreateBrandCommand>
{
    public CreateBrandCommandValidator(IRepository<BrandEntity> repository)
        : base(repository)
    {
    }
}