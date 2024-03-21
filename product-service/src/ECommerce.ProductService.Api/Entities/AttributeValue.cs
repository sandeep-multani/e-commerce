using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities;

public class AttributeValue
{
    [BsonElement("valueCode")]
    public string ValueCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}