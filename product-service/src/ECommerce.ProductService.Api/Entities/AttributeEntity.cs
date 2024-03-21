using ECommerce.ProductService.Api.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities;

[BsonCollection("attributes")]
public class AttributeEntity : Entity
{
    [BsonElement("attributeCode")]
    public string AttributeCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;

    [BsonElement("allowedValues")]
    public AttributeValue[] AllowedValues { get; set; } = default!;
}