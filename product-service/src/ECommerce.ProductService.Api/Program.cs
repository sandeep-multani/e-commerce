
using ECommerce.ProductService.Api.Extensions;

namespace ECommerce.ProductService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.ConfigureApiVersioning();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMongoDb(builder.Configuration);
        builder.Services
            .AddApplicationServices()
            .AddRepositories()
            .AddMappers();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}