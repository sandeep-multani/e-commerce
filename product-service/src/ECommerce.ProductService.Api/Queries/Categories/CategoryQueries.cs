using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Entities.Categories;
using ECommerce.ProductService.Api.Mappers;
using ECommerce.ProductService.Api.Models;
using ECommerce.ProductService.Api.Repositories;

namespace ECommerce.ProductService.Api.Queries.Categories;

public class CategoryQueries : ICategoryQueries
{
    private readonly ILogger<CategoryQueries> _logger;
    private readonly IRepository<CategoryEntity> _repository;

    public CategoryQueries(
        ILogger<CategoryQueries> logger,
        IRepository<CategoryEntity> repository)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<Category?> GetByIdAsync(string id)
    {
        var Category = await _repository.FindByIdAsync(ObjectIdMapper.ToObjectId(id));
        return CategoryMapper.EntityToModel(Category);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var CategoryEntities = _repository.FilterBy(Category => true);
        var Categorys = CategoryEntities.Select(x => CategoryMapper.EntityToModel(x)).ToList();
        return await Task.FromResult(Categorys);
    }
}