using ECommerce.ProductService.Api.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.ProductService.Api.Entities.Categories;

[BsonCollection("categories")]
public class CategoryEntity : Entity
{
    [BsonElement("categoryCode")]
    public string CategoryCode { get; set; } = default!;

    [BsonElement("name")]
    public string Name { get; set; } = default!;
}