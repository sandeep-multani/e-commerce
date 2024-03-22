namespace ECommerce.ProductService.Api.Commands.Products;

public abstract class ProductCommandBase : CommandBase
{
    public string Sku { get; set; } = default!;
    
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public string? PictureUri { get; set; }

    public int QuantityInStock { get; set; }

    public int RestockThreshold { get; set; }

    public int MaxStockThreshold { get; set; }

    public string? ProductType { get; set; }

    public string? ProductBrand { get; set; }
}