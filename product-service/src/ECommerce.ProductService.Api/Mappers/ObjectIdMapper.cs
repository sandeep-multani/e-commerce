using ECommerce.ProductService.Api.Exceptions;
using MongoDB.Bson;

namespace ECommerce.ProductService.Api.Mappers;

public static class ObjectIdMapper
{
    public static string ToHexString(ObjectId id) => id.ToString();
    public static ObjectId ToObjectId(string id)
    {
        try
        {
            return new ObjectId(id);
        }
        catch (Exception ex)
        {
            throw new InvalidMongoDbObjectIdException($"Invalid mongodb ObjectId: {id}", ex);
        }
    }
}