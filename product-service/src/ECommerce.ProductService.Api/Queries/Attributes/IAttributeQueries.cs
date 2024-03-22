using Attribute = ECommerce.ProductService.Api.Models.Attribute;

namespace ECommerce.ProductService.Api.Queries.Attributes;

public interface IAttributeQueries
{
    Task<IEnumerable<Attribute>> GetAllAsync();
    Task<Attribute?> GetByIdAsync(string id);
}
