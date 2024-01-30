using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BusCatalog.Api.Infrastructure;

public static class Cors
{
    public static IServiceCollection AddCorsPolicy(this IHostApplicationBuilder builder) =>
        builder.Services
            .AddCors(options =>
                CorsPolicy(
                    options,
                    builder.Configuration.GetValue<string>(ConfigurationKeys.SpaUrl)!));

    private static void CorsPolicy(CorsOptions options, string spaUrl) =>
        options.AddDefaultPolicy(policy =>
            policy
                .WithOrigins(spaUrl)
                .AllowAnyHeader()
                .AllowAnyMethod());
}