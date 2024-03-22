using ECommerce.ProductService.Api.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities.Brands;

[BsonCollection("brands")]
public class BrandEntity : Entity
{
    [BsonElement("brandCode")]
    public string BrandCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}