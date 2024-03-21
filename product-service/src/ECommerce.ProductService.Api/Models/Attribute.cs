namespace ECommerce.ProductService.Api.Models;

public class Attribute
{
    public string Id { get; set; } = default!;
    public string AttributeCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public AttributeValue[] AllowedValues { get; set; } = default!;
}