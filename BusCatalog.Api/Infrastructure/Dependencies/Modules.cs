using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Infrastructure;

public static class Modules
{
    public static IServiceCollection AddModules(this IServiceCollection services) =>
        services
            .AddVehicles()
            .AddPlaces()
            .AddLines();
}