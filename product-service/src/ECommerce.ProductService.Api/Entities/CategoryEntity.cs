using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Entities;

[BsonCollection("categories")]
public class CategoryEntity : Entity
{
    public string? Category { get; set; }
    public string? Name { get; set; }
}
