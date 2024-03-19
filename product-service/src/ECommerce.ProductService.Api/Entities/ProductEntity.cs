using ECommerce.ProductService.Api.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities;

[BsonCollection("products")]
public class ProductEntity : Entity
{
    [BsonElement("sku")]
    public string? Sku { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

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
    public string? Category { get; set; }

    [BsonElement("brand")]
    public string? Brand { get; set; }

    [BsonElement("attributes")]
    public IDictionary<string, string>? Attributes { get; set; }
}