using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace ECommerce.ProductService.Api.Models;

public class Product
{
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? PictureUri { get; set; }

    public ProductType? ProductType { get; set; }

    public ProductBrand? ProductBrand { get; set; }

    public int QuantityInStock { get; set; }

    public int RestockThreshold { get; set; }

    public int MaxStockThreshold { get; set; }

    public static Product Create(string name, string description)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name)),
            Description = Guard.Against.NullOrWhiteSpace(description, nameof(description))
        };
    }
}