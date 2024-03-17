using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Documents;

[BsonCollection("brands")]
public class BrandDocument : Document
{
    public string? Brand { get; set; }
    public string? Name { get; set; }
}