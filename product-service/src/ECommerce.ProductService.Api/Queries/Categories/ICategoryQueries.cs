using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Queries.Categories;

public interface ICategoryQueries
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(string id);
}
