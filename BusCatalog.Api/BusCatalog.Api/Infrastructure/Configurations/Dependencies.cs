using BusCatalog.Api.Adapters.Database;
using BusCatalog.Api.Domain.HealthCheck;
using BusCatalog.Api.Domain.Lines.Configurations;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Dependencies
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder AddAdapters() => builder.AddDatabase();
    
        public IServiceCollection AddDomain() =>
            builder.Services
                .AddVehicles()
                .AddLines(builder.Configuration)
                .AddHealthCheck();
    }
}
