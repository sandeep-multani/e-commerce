using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Products;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Products;

public class UpdateProductCommandHandler : CommandHandlerBase, ICommandHandler<UpdateProductCommand>
{
    private readonly IValidator<UpdateProductCommand> _commandValidaor;
    private readonly IRepository<ProductEntity> _repository;

    public UpdateProductCommandHandler(
        IValidator<UpdateProductCommand> commandValidaor,
        IRepository<ProductEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(UpdateProductCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var product = ProductMapper.CommandToEntity(command);
            await _repository.ReplaceOneAsync(product);
        }

        return Return();
    }
}