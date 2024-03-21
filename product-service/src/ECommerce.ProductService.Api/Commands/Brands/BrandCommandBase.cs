namespace ECommerce.ProductService.Api.Commands.Brands;

public abstract class BrandCommandBase : CommandBase
{
    public string BrandCode { get; set; } = default!;
    public string Name { get; set; } = default!;
}