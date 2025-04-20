using BusCatalog.Api.Adapters.Database;
using BusCatalog.Api.Domain.HealthCheck;
using BusCatalog.Api.Domain.Lines.Configurations;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Dependencies
{
    public static IHostApplicationBuilder AddAdapters(this IHostApplicationBuilder builder) =>
        builder.AddDatabase();
    
    public static IServiceCollection AddDomain(this IHostApplicationBuilder builder) =>
        builder.Services
            .AddVehicles()
            .AddLines(builder.Configuration)
            .AddHealthCheck();
}
