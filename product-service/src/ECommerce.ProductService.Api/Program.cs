
using ECommerce.ProductService.Api.Exceptions;
using ECommerce.ProductService.Api.Extensions;
using Hellang.Middleware.ProblemDetails;

namespace ECommerce.ProductService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.ConfigureApiVersioning();
        builder.Services.AddSwaggerGen();

        builder.Services.AddProblemDetails(opts =>
        {
            opts.IncludeExceptionDetails = (context, ex) =>
            {
                var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                return environment.IsDevelopment();
            };
        });

        builder.Services.AddMongoDb(builder.Configuration);
        builder.Services
            .AddValidators()
            .AddCommandHandlers()
            .AddQueries()
            .AddRepositories();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseProblemDetails();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}