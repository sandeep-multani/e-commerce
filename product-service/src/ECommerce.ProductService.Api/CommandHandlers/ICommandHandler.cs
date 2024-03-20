using ECommerce.ProductService.Api.Commands;

namespace ECommerce.ProductService.Api.CommandHandlers;

public interface ICommandHandler<in T> where T : CommandBase
{
    Task<CommandResult> Handle(T command);
}