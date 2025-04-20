namespace BusCatalog.Api.Domain.HealthCheck;

public static class Configuration
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services)
    {
        services.AddHttpClient<IHealthCheckAdapter, HealthCheckAdapter>();

        return services.AddScoped<IHealthCheckService, HealthCheckService>();
    }
}