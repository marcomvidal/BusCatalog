using System.Reflection;
using Microsoft.OpenApi.Models;

namespace BusCatalog.Api.Infrastructure;

public static class Swagger
{
    public static IServiceCollection AddSwaggerWithConfiguration(
        this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BusCatalog.Api",
                Description = "This API keeps BusCatalog data source."
            });

            options.EnableAnnotations();
        });
}
