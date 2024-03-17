using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Documents;

[BsonCollection("products")]
public class ProductDocument : Document
{
    public string? Sku { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUri { get; set; }
    public int QuantityInStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public IDictionary<string, string>? Attributes { get; set; }
}