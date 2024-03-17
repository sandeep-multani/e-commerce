namespace ECommerce.ProductService.Api.Configurations;

public class MongoDbConfiguration
{
    public required string DatabaseName { get; set; }
    public required string ConnectionString { get; set; }
}