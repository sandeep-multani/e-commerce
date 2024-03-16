using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Services;

public class ProductService : IProductService
{ 
    private readonly List<Product> _products = [
        new Product{
            Id = Guid.NewGuid(),
            Name = "Product 1",
            Description = "Product 1 description",
        },
        new Product{
            Id = Guid.NewGuid(),
            Name = "Product 2",
            Description = "Product 2 description",
        },
    ];
    
    public async Task<Product?> GetProductAsync(Guid id)
    {
        var product = _products.Where(x => x.Id == id).FirstOrDefault();
        return await Task.FromResult(product);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await Task.FromResult(_products);
    }   
}