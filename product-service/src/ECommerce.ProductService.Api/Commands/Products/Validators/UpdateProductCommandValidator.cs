using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public class UpdateProductCommandValidator : ProductCommandValidatorBase<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IRepository<ProductEntity> repository)
    : base(repository)
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
