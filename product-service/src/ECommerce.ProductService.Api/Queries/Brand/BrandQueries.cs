using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Brands;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Models;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Queries.Brands;

public class BrandQueries : IBrandQueries
{
    private readonly ILogger<BrandQueries> _logger;
    private readonly IRepository<BrandEntity> _repository;

    public BrandQueries(
        ILogger<BrandQueries> logger,
        IRepository<BrandEntity> repository)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<Brand?> GetByIdAsync(string id)
    {
        var Brand = await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id));
        return BrandMapper.EntityToModel(Brand);
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        var BrandEntities = _repository.FilterBy(Brand => true);
        var Brands = BrandEntities.Select(x => BrandMapper.EntityToModel(x)).ToList();
        return await Task.FromResult(Brands);
    }
}