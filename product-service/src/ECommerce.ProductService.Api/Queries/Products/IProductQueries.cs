using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Queries.Products;

public interface IProductQueries
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(string id);
}