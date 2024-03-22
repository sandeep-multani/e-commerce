using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities.Products;

public class ProductBrand
{
    [BsonElement("brandCode")]
    public string BrandCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}