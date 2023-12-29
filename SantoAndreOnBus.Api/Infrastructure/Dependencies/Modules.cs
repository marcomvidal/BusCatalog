using SantoAndreOnBus.Api.Domain.Lines;
using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure;

public static class Modules
{
    public static IServiceCollection AddModules(this IServiceCollection services) =>
        services
            .AddVehicles()
            .AddPlaces()
            .AddLines();
}