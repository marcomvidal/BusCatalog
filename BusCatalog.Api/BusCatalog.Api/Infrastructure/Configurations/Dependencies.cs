using BusCatalog.Api.Adapters.Database;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Dependencies
{
    public static IHostApplicationBuilder AddAdapters(this IHostApplicationBuilder builder) =>
        builder.AddDatabase();
    
    public static IServiceCollection AddDomain(this IServiceCollection services) =>
        services
            .AddVehicles()
            .AddLines();
}
