using System.Linq.Expressions;
using ECommerce.ProductService.Api.Entities;

namespace ECommerce.ProductService.Api.Mappers;

public interface IMapper<TEntity, TModel> 
where TEntity : IEntity 
where TModel : class
{
    Expression<Func<TEntity, TModel>> ModelProjection { get; }
    Expression<Func<TModel, TEntity>> EntityProjection { get; }
    TModel? Map(TEntity entity);
    TEntity? Map(TModel model);
}
