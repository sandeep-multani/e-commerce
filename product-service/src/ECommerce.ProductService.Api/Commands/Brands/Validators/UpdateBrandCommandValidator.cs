using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Brands.Validators;

public class UpdateBrandCommandValidator : BrandCommandValidatorBase<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator(IRepository<BrandEntity> repository)
    : base(repository)
    {
        ValidateId();
    }

    private void ValidateId()
    {
        RuleFor(command => command.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Brand Id can't be empty");
    }
}
