using ECommerce.ProductService.Api.Commands;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.ProductService.Api.CommandHandlers;

public abstract class CommandHandlerBase
{
    protected IEnumerable<string> Notifications = default!;

    protected async Task<ValidationResult> Validate<T, TValidator>(
        T command,
        TValidator validator)
        where T : CommandBase
        where TValidator : IValidator<T>
    {
        var validationResult = await validator.ValidateAsync(command);
        Notifications = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

        return validationResult;
    }

    public CommandResult Return() => new CommandResult(!Notifications.Any(), Notifications);
}
