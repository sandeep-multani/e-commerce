using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Categories.Validators;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    private readonly IRepository<CategoryEntity> _repository;

    public DeleteCategoryCommandValidator(IRepository<CategoryEntity> repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));

        ValidateExists();
    }

    private void ValidateExists()
    {
        RuleFor(command => command.Id)
        .MustAsync(async (id, cancellationToken) => await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Category with id '{x.Id}' does not exists");
    }
}