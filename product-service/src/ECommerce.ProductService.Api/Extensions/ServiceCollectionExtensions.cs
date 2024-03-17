using Asp.Versioning;
using ECommerce.ProductService.Api.Configurations;
using ECommerce.ProductService.Api.Repositories;
using ECommerce.ProductService.Api.Services;
using Microsoft.Extensions.Options;
using ProductServiceClass = ECommerce.ProductService.Api.Services.ProductService;

namespace ECommerce.ProductService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbConfiguration>(configuration.GetSection(ConfigurationConstants.MongoDbConfiguration));
        services.AddSingleton(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbConfiguration>>().Value);
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services){
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services.AddSingleton<IProductService, ProductServiceClass>();
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        return services;
    }
}