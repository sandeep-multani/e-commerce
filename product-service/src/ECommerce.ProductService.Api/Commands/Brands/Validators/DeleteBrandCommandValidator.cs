using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Brands.Validators;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    private readonly IRepository<BrandEntity> _repository;

    public DeleteBrandCommandValidator(IRepository<BrandEntity> repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));

        ValidateExists();
    }

    private void ValidateExists()
    {
        RuleFor(command => command.Id)
        .MustAsync(async (id, cancellationToken) => await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Brand with id '{x.Id}' does not exists");
    }
}