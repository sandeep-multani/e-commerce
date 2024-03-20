namespace ECommerce.ProductService.Api.CommandHandlers;

public class CommandResult
{
    public readonly IEnumerable<string> Errors;
    public readonly bool Success;

    public CommandResult(bool success, IEnumerable<string> errors)
    {
        Success = success;
        Errors = errors;
    }
}