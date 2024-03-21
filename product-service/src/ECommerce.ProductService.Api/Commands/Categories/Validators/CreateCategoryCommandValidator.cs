using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Commands.Categories.Validators;

public class CreateCategoryCommandValidator : CategoryCommandValidatorBase<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(IRepository<CategoryEntity> repository)
        : base(repository)
    {
    }
}