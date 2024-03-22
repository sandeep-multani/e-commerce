using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.Commands.Brands.Validators;

public class UpdateBrandCommandValidator : BrandCommandValidatorBase<UpdateBrandCommand>
{
    private readonly IRepository<BrandEntity> _repository;

    public UpdateBrandCommandValidator(IRepository<BrandEntity> repository)
    : base(repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        ValidateId();
        ValidateCodeNotChanged();
    }

    private void ValidateId()
    {
        RuleFor(command => command.Id)
            .Must(id => !string.IsNullOrWhiteSpace(id))
            .WithSeverity(Severity.Error)
            .WithMessage("Brand Id can't be empty");
    }

    private void ValidateCodeNotChanged()
    {
        RuleFor(command => command)
        .MustAsync(async (command, cancellationToken) =>
                await _repository.FindOneAsync(x => x.BrandCode == command.BrandCode && x.Id == ObjectIdMapper.ToObjectId(command.Id)) != null)
            .WithSeverity(Severity.Error)
            .WithMessage(x => $"Brand with id '{x.Id}' and code '{x.BrandCode}' does not exists. Brand code can't be changed.");
    }
}
