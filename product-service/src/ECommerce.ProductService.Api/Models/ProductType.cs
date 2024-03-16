using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace ECommerce.ProductService.Api.Models;

public class ProductType
{
    public Guid Id { get; set; }

    [Required]
    public string? Type { get; set; }

    public static ProductType Create(string type)
    {
        return new ProductType
        {
            Type = Guard.Against.NullOrWhiteSpace(type, nameof(type))
        };
    }
}
