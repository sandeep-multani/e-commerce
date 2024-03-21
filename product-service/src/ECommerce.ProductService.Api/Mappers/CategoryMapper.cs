using AutoMapper;
using ECommerce.ProductService.Api.Commands.Categories;
using ECommerce.ProductService.Api.Entities;
using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Mappers;

public static class CategoryMapper
{
    public static CategoryEntity CommandToEntity(CreateCategoryCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<CreateCategoryCommand, CategoryEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<CategoryEntity>(command);
    }

    public static CategoryEntity CommandToEntity(UpdateCategoryCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UpdateCategoryCommand, CategoryEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<CategoryEntity>(command);
    }

    public static Category EntityToModel(CategoryEntity entity)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<CategoryEntity, Category>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<Category>(entity);
    }
}