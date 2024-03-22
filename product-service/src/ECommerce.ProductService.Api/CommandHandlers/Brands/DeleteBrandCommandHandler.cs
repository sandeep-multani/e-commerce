using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Commands.Brands;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;

namespace ECommerce.ProductService.Api.CommandHandlers.Brands;

public class DeleteBrandCommandHandler : CommandHandlerBase, ICommandHandler<DeleteBrandCommand>
{
    private readonly IValidator<DeleteBrandCommand> _commandValidaor;
    private readonly IRepository<BrandEntity> _repository;

    public DeleteBrandCommandHandler(
        IValidator<DeleteBrandCommand> commandValidaor,
        IRepository<BrandEntity> repository)
    {
        _commandValidaor = Guard.Against.Null(commandValidaor, nameof(commandValidaor));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<CommandResult> HandleAsync(DeleteBrandCommand command)
    {
        var validationResult = await ValidateAsync(command, _commandValidaor);

        if (validationResult.IsValid)
        {
            await _repository.DeleteByIdAsync(ObjectIdMapper.ToObjectId(command.Id));
        }

        return Return();
    }
}