using System.Linq.Expressions;
using ECommerce.ProductService.Api.Entities;
using MongoDB.Bson;

namespace ECommerce.ProductService.Api.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> AsQueryable();

    IEnumerable<TEntity> FilterBy(
        Expression<Func<TEntity, bool>> filterExpression);

    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression);

    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task<TEntity> FindByIdAsync(ObjectId id);

    Task InsertOneAsync(TEntity document);

    Task InsertManyAsync(ICollection<TEntity> documents);

    Task ReplaceOneAsync(TEntity document);

    Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task DeleteByIdAsync(ObjectId id);

    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
}
