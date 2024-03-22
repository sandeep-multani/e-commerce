namespace ECommerce.ProductService.Api.Commands.Attributes;

public abstract class AttributeCommandBase : CommandBase
{
    public string AttributeCode { get; set; } = default!;
    public string Name { get; set; } = default!;
}