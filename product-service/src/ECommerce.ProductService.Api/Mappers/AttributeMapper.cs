using AutoMapper;
using ECommerce.ProductService.Api.Commands.Attributes;
using ECommerce.ProductService.Api.Entities;
using Attribute = ECommerce.ProductService.Api.Models.Attribute;

namespace ECommerce.ProductService.Api.Mappers;

public static class AttributeMapper
{
    public static AttributeEntity CommandToEntity(CreateAttributeCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<CreateAttributeCommand, AttributeEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<AttributeEntity>(command);
    }

    public static AttributeEntity CommandToEntity(UpdateAttributeCommand command)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UpdateAttributeCommand, AttributeEntity>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<AttributeEntity>(command);
    }

    public static Attribute EntityToModel(AttributeEntity entity)
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.CreateMap<AttributeEntity, Attribute>();
        });

        var mapper = config.CreateMapper();
        return mapper.Map<Attribute>(entity);
    }
}