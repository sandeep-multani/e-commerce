namespace ECommerce.ProductService.Api.Models;

public class ProductAttribute
{
    public string AttributeCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public AttributeValue Value { get; set; } = default!;
}
