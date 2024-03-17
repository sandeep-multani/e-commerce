using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Documents;
using ECommerce.ProductService.Api.Models;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Services;

public class ProductService : IProductService
{ 
    private readonly ILogger<ProductService> _logger;
    private readonly IRepository<ProductDocument> _productRepository;

    public ProductService(ILogger<ProductService> logger, IRepository<ProductDocument> productRepository)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
    }

    public Task<Product> CreateProductAsync(Product product)
    {
        _products.Add(product);
    }

    public Task DeleteProductAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetProductAsync(Guid id)
    {
        var product = _products.Where(x => x.Id == id).FirstOrDefault();
        return await Task.FromResult(product);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await Task.FromResult(_products);
    }

    public Task<Product> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}