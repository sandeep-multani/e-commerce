namespace ECommerce.ProductService.Api.Commands.Categories;

public class UpdateCategoryCommand : CategoryCommandBase
{
    public required string Id { get; set; }
}