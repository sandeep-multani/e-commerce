namespace ECommerce.ProductService.Api.Commands.Products;

public class UpdateProductCommand : ProductCommandBase
{
    public required string Id { get; set; }
}