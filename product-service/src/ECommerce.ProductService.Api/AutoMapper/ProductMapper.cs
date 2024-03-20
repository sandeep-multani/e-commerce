using AutoMapper;
using ECommerce.ProductService.Api.Commands.Products;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Mappers;

public static class ProductMapper
{
    public static ProductEntity CommandToEntity(CreateProductCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<CreateProductCommand, ProductEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<ProductEntity>(command);
    }

    public static ProductEntity CommandToEntity(UpdateProductCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UpdateProductCommand, ProductEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<ProductEntity>(command);
    }

    public static Product EntityToModel(ProductEntity entity)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<ProductEntity, Product>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<Product>(entity);
    }
}