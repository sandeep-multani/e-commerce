using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductAsync(string id);
    //Task<Product> CreateProductAsync(Product product);
    //Task<Product> UpdateProductAsync(Product product);
    //Task DeleteProductAsync(Guid id);
}
