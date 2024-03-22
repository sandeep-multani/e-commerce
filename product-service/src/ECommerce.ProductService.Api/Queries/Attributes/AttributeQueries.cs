using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Repositories;
using Attribute = ECommerce.ProductService.Api.Models.Attribute;

namespace ECommerce.ProductService.Api.Queries.Attributes;

public class AttributeQueries : IAttributeQueries
{
    private readonly ILogger<AttributeQueries> _logger;
    private readonly IRepository<AttributeEntity> _repository;

    public AttributeQueries(
        ILogger<AttributeQueries> logger,
        IRepository<AttributeEntity> repository)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<Attribute?> GetByIdAsync(string id)
    {
        var Attribute = await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id));
        return AttributeMapper.EntityToModel(Attribute);
    }

    public async Task<IEnumerable<Attribute>> GetAllAsync()
    {
        var AttributeEntities = _repository.FilterBy(Attribute => true);
        var Attributes = AttributeEntities.Select(x => AttributeMapper.EntityToModel(x)).ToList();
        return await Task.FromResult(Attributes);
    }
}