namespace ECommerce.ProductService.Api.Commands.Attributes;

public class UpdateAttributeCommand : AttributeCommandBase
{
    public required string Id { get; set; }
}