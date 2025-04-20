using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Cors
{
    public static IServiceCollection AddCorsPolicy(this IHostApplicationBuilder builder) =>
        builder.Services.AddCors(options => CorsPolicy(
            options,
            [
                builder.Configuration.GetValue<string>(ConfigurationKeys.SpaUrl)!,
                builder.Configuration.GetValue<string>(ConfigurationKeys.ScraperUrl)!
            ]));
        

    private static void CorsPolicy(CorsOptions options, string[] urls) =>
        options.AddDefaultPolicy(policy =>
            policy
                .WithOrigins(urls)
                .AllowAnyHeader()
                .AllowAnyMethod());
}