using ECommerce.ProductService.Api.Entities.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities.Products;

public class ProductAttribute
{
    [BsonElement("attributeCode")]
    public string AttributeCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("attributeValue")]
    public AttributeValue AttributeValue { get; set; } = default!;
}