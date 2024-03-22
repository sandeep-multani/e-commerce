namespace ECommerce.ProductService.Api.Commands.Attributes;

public class DeleteAttributeCommand : CommandBase
{
    public required string Id { get; set; }
}