using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Products;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Products;

public class DeleteProductCommandHandler : CommandHandlerBase, ICommandHandler<DeleteProductCommand>
{
    private readonly IValidator<DeleteProductCommand> _commandValidaor;
    private readonly IRepository<ProductEntity> _repository;

    public DeleteProductCommandHandler(
        IValidator<DeleteProductCommand> commandValidaor,
        IRepository<ProductEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(DeleteProductCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            await _repository.DeleteByIdAsync(command.Id);
        }

        return Return();
    }
}