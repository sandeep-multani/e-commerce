using AutoMapper;
using ECommerce.ProductService.Api.Commands.Brands;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Mappers;

public static class BrandMapper
{
    public static BrandEntity CommandToEntity(CreateBrandCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<CreateBrandCommand, BrandEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<BrandEntity>(command);
    }

    public static BrandEntity CommandToEntity(UpdateBrandCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UpdateBrandCommand, BrandEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<BrandEntity>(command);
    }

    public static Brand EntityToModel(BrandEntity entity)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<BrandEntity, Brand>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<Brand>(entity);
    }
}