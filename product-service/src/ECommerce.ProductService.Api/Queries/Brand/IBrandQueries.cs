using ECommerce.ProductService.Api.Models;

namespace ECommerce.ProductService.Api.Queries.Brands;

public interface IBrandQueries
{
    Task<IEnumerable<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(string id);
}
