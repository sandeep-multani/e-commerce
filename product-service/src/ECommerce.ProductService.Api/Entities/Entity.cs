using MongoDB.Bson;

namespace ECommerce.ProductService.Api.Entities;

public abstract class Entity : IEntity
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
}
