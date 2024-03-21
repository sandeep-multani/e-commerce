namespace ECommerce.ProductService.Api.Commands.Brands;

public class UpdateBrandCommand : BrandCommandBase
{
    public required string Id { get; set; }
}