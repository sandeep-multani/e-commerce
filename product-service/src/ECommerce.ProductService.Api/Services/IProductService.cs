using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Services;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductAsync(Guid id);
}
