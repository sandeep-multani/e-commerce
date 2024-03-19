using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Entities;

[BsonCollection("attributes")]
public class AttributeEntity : Entity
{
    public string? Attribute { get; set; }
    public string? Name { get; set; }
    public string[]? Values { get; set; }
}
