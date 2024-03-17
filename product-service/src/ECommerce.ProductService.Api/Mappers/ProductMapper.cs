using System.Linq.Expressions;
using ECommerce.ProductService.Api.Documents;
using ECommerce.ProductService.Api.Models;
using Mapster;

namespace ECommerce.ProductService.Api.Mappers;

public static class ProductMapper {
    public static Product AdaptToModel(this ProductDocument document) 
        => document.Adapt<Product>();
        
    public static Product AdaptToModel(this ProductDocument document, Product model)
        => document.Adapt(model);

    public static Expression<Func<ProductDocument, Product>> ProjectToModel = 
        p => p.ProjectToType<Product>();
}