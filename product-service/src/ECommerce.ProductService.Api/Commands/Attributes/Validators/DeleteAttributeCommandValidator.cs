using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Attributes;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Attributes.Validators;

public class DeleteAttributeCommandValidator : AbstractValidator<DeleteAttributeCommand>
{
    private readonly IRepository<AttributeEntity> _repository;

    public DeleteAttributeCommandValidator(IRepository<AttributeEntity> repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));

        ValidateExists();
    }

    private void ValidateExists()
    {
        RuleFor(command => command.Id)
        .MustAsync(async (id, cancellationToken) => await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Attribute with id '{x.Id}' does not exists");
    }
}