using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Documents;

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }
    DateTime CreatedAt { get; }
    DateTime LastUpdatedAt { get; }
}
