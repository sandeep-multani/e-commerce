using System.Linq.Expressions;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Models;
using MongoDB.Bson;

namespace ECommerce.ProductService.Api.Mappers;

public interface IProductMapper : IMapper<ProductEntity, Product> { }

public class ProductMapper : IProductMapper
{
    public Product? Map(ProductEntity entity)
    {
        return entity == null ? null : new Product()
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Description = entity.Description
        };
    }

    public ProductEntity? Map(Product product)
    {
        return product == null ? null : new ProductEntity()
        {
            Id = ObjectId.Parse(product.Id),
            Name = product.Name,
            Description = product.Description
        };
    }

    public Expression<Func<ProductEntity, Product>> ModelProjection
        => pd => Map(pd) ?? new Product();

    public Expression<Func<Product, ProductEntity>> EntityProjection
        => p => Map(p) ?? new ProductEntity();
}