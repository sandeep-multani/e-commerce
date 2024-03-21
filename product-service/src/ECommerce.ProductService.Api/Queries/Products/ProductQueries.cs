using Ardalis.GuardClauses;
using AutoMapper;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Models;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Queries.Products;

public class ProductQueries : IProductQueries
{
    private readonly ILogger<ProductQueries> _logger;
    private readonly IRepository<ProductEntity> _repository;

    public ProductQueries(
        ILogger<ProductQueries> logger,
        IRepository<ProductEntity> repository)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var product = await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id));
        return ProductMapper.EntityToModel(product);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var productEntities = _repository.FilterBy(product => true);
        var products = productEntities.Select(x => ProductMapper.EntityToModel(x)).ToList();
        return await Task.FromResult(products);
    }
}
