using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public class ProductAttributeValidator : AbstractValidator<(string AttributeCode, string AttributeValueCode)>
{
    private readonly IRepository<AttributeEntity> _attributeRepository;

    public ProductAttributeValidator(IRepository<AttributeEntity> attributeRepository)
    {
        _attributeRepository = Guard.Against.Null(attributeRepository, nameof(attributeRepository));
        ValidateProductAttribute();
    }

    private void ValidateProductAttribute()
    {
        RuleFor(attribute => attribute)
            .MustAsync(async (attribute, cancellationToken) =>
                await _attributeRepository.FindOneAsync(a =>
                    a.AttributeCode == attribute.AttributeCode &&
                    a.AllowedValues.Any(v => v.ValueCode == attribute.AttributeValueCode)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Attribute with code '{x.AttributeCode}' and value '{x.AttributeValueCode}' does not exists");
    }
}