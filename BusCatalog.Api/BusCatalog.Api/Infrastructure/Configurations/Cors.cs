using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Cors
{
    public static IServiceCollection AddCorsPolicy(this IHostApplicationBuilder builder)
    {
        string[] urls = [
            builder.Configuration.GetValue<string>(ConfigurationKeys.SpaUrl)!,
            builder.Configuration.GetValue<string>(ConfigurationKeys.ScraperUrl)!
        ];

        return builder.Services.AddCors(options => CorsPolicy(options, urls));
    }
        

    private static void CorsPolicy(CorsOptions options, string[] urls) =>
        options.AddDefaultPolicy(policy =>
            policy
                .WithOrigins(urls)
                .AllowAnyHeader()
                .AllowAnyMethod());
}