namespace ECommerce.ProductService.Api.Models;

public class Product
{
    public string Id { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string? PictureUri { get; set; }
    public int QuantityInStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public Category Category { get; set; } = default!;
    public Brand Brand { get; set; } = default!;
    public ProductAttribute[] Attributes { get; set; } = default!;
}