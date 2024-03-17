using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Documents;

[BsonCollection("categories")]
public class CategoryDocument : Document
{
    public string? Category { get; set; }
    public string? Name { get; set; }
}
