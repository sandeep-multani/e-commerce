namespace ECommerce.ProductService.Api.Commands.Categories;

public abstract class CategoryCommandBase : CommandBase
{
    public string CategoryCode { get; set; } = default!;
    public string Name { get; set; } = default!;
}