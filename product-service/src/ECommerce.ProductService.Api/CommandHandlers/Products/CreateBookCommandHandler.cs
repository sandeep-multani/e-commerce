using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Products;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Products;

public class CreateProductCommandHandler : CommandHandlerBase, ICommandHandler<CreateProductCommand>
{
    private readonly IValidator<CreateProductCommand> _commandValidaor;
    private readonly IRepository<ProductEntity> _repository;

    public CreateProductCommandHandler(
        IValidator<CreateProductCommand> commandValidaor,
        IRepository<ProductEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> Handle(CreateProductCommand command)
    {
        var validationResult = await Validate(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            var product = ProductMapper.CommandToEntity(command);
            await _repository.InsertOneAsync(product);
        }

        return Return();
    }
}
