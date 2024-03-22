using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Attributes.Validators;

public class UpdateAttributeCommandValidator : AttributeCommandValidatorBase<UpdateAttributeCommand>
{
    private readonly IRepository<AttributeEntity> _repository;

    public UpdateAttributeCommandValidator(IRepository<AttributeEntity> repository)
    : base(repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));

        ValidateId();
        ValidateCodeNotChanged();
        ValidateValueCodeNotChanged();
    }

    private void ValidateId()
    {
        RuleFor(command => command.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Attribute Id can't be empty");
    }

    private void ValidateCodeNotChanged()
    {
        RuleFor(command => command)
        .MustAsync(async (command, cancellationToken) =>
                await _repository.FindOneAsync(x => x.AttributeCode == command.AttributeCode && x.Id == ObjectIdMapper.ToObjectId(command.Id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Attribute with id '{x.Id}' and code '{x.AttributeCode}' does not exists. Attribute code can't be changed.");
    }

    private void ValidateValueCodeNotChanged()
    {
        RuleForEach(command => command.AllowedValues)
        .MustAsync(async (command, attributeValue, cancellationToken) =>
                await _repository.FindOneAsync(x => 
                    x.AllowedValues.Any(v => v.ValueCode == attributeValue.ValueCode) && x.Id == ObjectIdMapper.ToObjectId(command.Id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Attribute with id '{x.Id}' and one of the values does not exists. Attribute value code can't be changed.");
    }
}
