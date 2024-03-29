using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Commands.Attributes.Validators;

public class CreateAttributeCommandValidator : AttributeCommandValidatorBase<CreateAttributeCommand>
{
    public CreateAttributeCommandValidator(IRepository<AttributeEntity> repository)
        : base(repository)
    {
    }
}