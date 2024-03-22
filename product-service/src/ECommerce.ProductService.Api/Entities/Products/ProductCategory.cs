using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities.Products;

public class ProductCategory
{
    [BsonElement("categoryCode")]
    public string CategoryCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}