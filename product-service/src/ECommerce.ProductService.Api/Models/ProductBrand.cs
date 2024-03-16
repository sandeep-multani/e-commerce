using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace ECommerce.ProductService.Api.Models;

public class ProductBrand {
    public Guid Id { get; set; }

    [Required]
    public string? Brand { get; set; }

    public static ProductBrand Create (string brand) {
        return new ProductBrand {
            Brand = Guard.Against.NullOrWhiteSpace(brand, nameof(brand))
        };
    }
}