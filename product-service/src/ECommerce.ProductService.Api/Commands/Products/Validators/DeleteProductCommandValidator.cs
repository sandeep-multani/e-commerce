using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Products.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IRepository<ProductEntity> _repository;

    public DeleteProductCommandValidator(IRepository<ProductEntity> repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));

        ValidateExists();
    }

    private void ValidateExists()
    {
        RuleFor(productBaseCommand => productBaseCommand.Id)
        .MustAsync(async (id, cancellationToken) => await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Product with id '{x.Id}' does not exists");
    }
}