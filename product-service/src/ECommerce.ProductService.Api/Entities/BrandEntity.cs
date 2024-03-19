using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Entities;

[BsonCollection("brands")]
public class BrandEntity : Entity
{
    public string? Brand { get; set; }
    public string? Name { get; set; }
}