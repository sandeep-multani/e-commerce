namespace ECommerce.ProductService.Api.CommandHandlers;

public class CommandResult
{
    public readonly IEnumerable<string> Errors;
    public readonly bool Success;
    public readonly string ResourceId = default!;

    public CommandResult(bool success, IEnumerable<string> errors)
    {
        Success = success;
        Errors = errors;
    }

    public CommandResult(bool success, IEnumerable<string> errors, string resourceId)
    {
        Success = success;
        Errors = errors;
        ResourceId = resourceId;
    }
}