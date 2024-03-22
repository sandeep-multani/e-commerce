using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Brands;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Brands;

public class CreateBrandCommandHandler : CommandHandlerBase, ICommandHandler<CreateBrandCommand>
{
    private readonly IValidator<CreateBrandCommand> _commandValidaor;
    private readonly IRepository<BrandEntity> _repository;

    public CreateBrandCommandHandler(
        IValidator<CreateBrandCommand> commandValidaor,
        IRepository<BrandEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(CreateBrandCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var brand = BrandMapper.CommandToEntity(command);
            await _repository.InsertOneAsync(brand);
            return Return(brand.Id.ToString());
        }

        return Return();
    }
}