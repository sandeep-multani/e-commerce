using ECommerce.ProductService.Api.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities.Products;

[BsonCollection("products")]
public class ProductEntity : Entity
{
    [BsonElement("sku")]
    public string Sku { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("description")]
    public string Description { get; set; } = default!;

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; }

    [BsonElement("quantityInStock")]
    public int QuantityInStock { get; set; }

    [BsonElement("restockThreshold")]
    public int RestockThreshold { get; set; }

    [BsonElement("maxStockThreshold")]
    public int MaxStockThreshold { get; set; }

    [BsonElement("category")]
    public ProductCategory Category { get; set; } = default!;

    [BsonElement("brand")]
    public ProductBrand Brand { get; set; } = default!;

    [BsonElement("attributes")]
    public ProductAttribute[] Attributes { get; set; } = default!;
}