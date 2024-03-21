using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Brands;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Brands;

public class UpdateBrandCommandHandler : CommandHandlerBase, ICommandHandler<UpdateBrandCommand>
{
    private readonly IValidator<UpdateBrandCommand> _commandValidaor;
    private readonly IRepository<BrandEntity> _repository;

    public UpdateBrandCommandHandler(
        IValidator<UpdateBrandCommand> commandValidaor,
        IRepository<BrandEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(UpdateBrandCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var brand = BrandMapper.CommandToEntity(command);
            await _repository.ReplaceOneAsync(brand);
        }

        return Return();
    }
}