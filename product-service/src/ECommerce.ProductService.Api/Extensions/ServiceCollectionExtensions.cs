using Asp.Versioning;
using ECommerce.ProductService.Api.CommandHandlers;
using ECommerce.ProductService.Api.CommandHandlers.Products;
using ECommerce.ProductService.Api.Commands.Products;
using ECommerce.ProductService.Api.Configurations;
using ECommerce.ProductService.Api.Queries.Products;
using ECommerce.ProductService.Api.Repositories;
using FluentValidation;
using Microsoft.Extensions.Options;
using ECommerce.ProductService.Api.Commands.Products.Validators;

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

    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>(); ;
        services.AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>(); ;
        services.AddScoped<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>(); ;
        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IProductQueries, ProductQueries>();
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
        services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }

    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
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