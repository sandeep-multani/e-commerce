using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities;

public interface IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }

    [BsonElement("createdAt")]
    DateTime CreatedAt { get; }
}
