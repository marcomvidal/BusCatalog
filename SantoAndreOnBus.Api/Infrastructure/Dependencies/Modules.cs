using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure;

public static class Modules
{
    public static IServiceCollection AddModules(this IServiceCollection services) =>
        services
            .AddVehicles()
            .AddPlaces()
            .AddLines();
}