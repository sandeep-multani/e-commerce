using MongoDB.Bson;

namespace ECommerce.ProductService.Api.Documents;

public abstract class Document : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
    public DateTime LastUpdatedAt { get; set; }
}
