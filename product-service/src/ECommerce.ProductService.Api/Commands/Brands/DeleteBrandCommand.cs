namespace ECommerce.ProductService.Api.Commands.Brands;

public class DeleteBrandCommand : CommandBase
{
    public required string Id { get; set; }
}