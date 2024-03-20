namespace ECommerce.ProductService.Api.Commands.Products;

public class DeleteProductCommand : CommandBase
{
    public required string Id { get; set; }
}