using System.Collections.Immutable;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Models;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Services;

public class ProductService : IProductService
{ 
    private readonly ILogger<ProductService> _logger;
    private readonly IRepository<ProductEntity> _repository;
    private readonly IProductMapper _mapper;

    public ProductService(
        ILogger<ProductService> logger, 
        IRepository<ProductEntity> repository,
        IProductMapper mapper)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Product?> GetProductAsync(string id)
    {
        var product = await _repository.FindByIdAsync(id);
        return _mapper.Map(product);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = _repository.AsQueryable().ToList().Select(_mapper.ModelProjection);
        return await Task.FromResult(products);
    }
}