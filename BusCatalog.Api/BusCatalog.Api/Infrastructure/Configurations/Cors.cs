using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Cors
{    
    extension(IHostApplicationBuilder builder)
    {
        public IServiceCollection AddCorsPolicy() =>
            builder.Services.AddCors(options => CorsPolicy(
            options,
            [
                builder.Configuration.GetValue<string>(ConfigurationKeys.SpaUrl)!,
                builder.Configuration.GetValue<string>(ConfigurationKeys.ScraperUrl)!
            ]));
    }
    
    private static void CorsPolicy(CorsOptions options, string[] urls) =>
        options.AddDefaultPolicy(policy =>
            policy
                .WithOrigins(urls)
                .AllowAnyHeader()
                .AllowAnyMethod());
}