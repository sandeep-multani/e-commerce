namespace ECommerce.ProductService.Api.Models;

public class Product
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? PictureUri { get; set; }

    public ProductType? ProductType { get; set; }

    public ProductBrand? ProductBrand { get; set; }

    public int QuantityInStock { get; set; }

    public int RestockThreshold { get; set; }

    public int MaxStockThreshold { get; set; }
}