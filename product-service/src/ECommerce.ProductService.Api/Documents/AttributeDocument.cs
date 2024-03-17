using ECommerce.ProductService.Api.Attributes;

namespace ECommerce.ProductService.Api.Documents;

[BsonCollection("attributes")]
public class AttributeDocument : Document
{
    public string? Attribute { get; set; }
    public string? Name { get; set; }
    public string[]? Values { get; set; }
}
