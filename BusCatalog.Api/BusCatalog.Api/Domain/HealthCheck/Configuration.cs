namespace BusCatalog.Api.Domain.HealthCheck;

public static class Configuration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHealthCheck()
        {
            services.AddHttpClient<IHealthCheckAdapter, HealthCheckAdapter>();

            return services.AddScoped<IHealthCheckService, HealthCheckService>();
        }
    }
}