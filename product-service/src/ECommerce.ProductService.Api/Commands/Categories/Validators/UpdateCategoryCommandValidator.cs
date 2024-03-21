using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Categories.Validators;

public class UpdateCategoryCommandValidator : CategoryCommandValidatorBase<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(IRepository<CategoryEntity> repository)
    : base(repository)
    {
        ValidateId();
    }

    private void ValidateId()
    {
        RuleFor(command => command.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Category Id can't be empty");
    }
}
