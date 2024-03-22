using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Attributes.Validators;

public class UpdateAttributeCommandValidator : AttributeCommandValidatorBase<UpdateAttributeCommand>
{
    public UpdateAttributeCommandValidator(IRepository<AttributeEntity> repository)
    : base(repository)
    {
        ValidateId();
    }

    private void ValidateId()
    {
        RuleFor(command => command.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Attribute Id can't be empty");
    }
}
