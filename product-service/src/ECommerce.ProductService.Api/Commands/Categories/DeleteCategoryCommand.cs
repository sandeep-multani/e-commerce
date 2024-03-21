namespace ECommerce.ProductService.Api.Commands.Categories;

public class DeleteCategoryCommand : CommandBase
{
    public required string Id { get; set; }
}