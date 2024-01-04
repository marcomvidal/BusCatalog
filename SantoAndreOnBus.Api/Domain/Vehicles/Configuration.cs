using FluentValidation;

namespace SantoAndreOnBus.Api.Domain.Vehicles;

public static class Configuration
{
    public static IServiceCollection AddVehicles(this IServiceCollection services) =>
        services
            .AddScoped<IVehicleRepository, VehicleRepository>()
            .AddScoped<IVehicleService, VehicleService>()
            .AddScoped<IValidator<VehiclePostRequest>, VehiclePostValidator>()
            .AddScoped<IValidator<VehiclePutRequest>, VehiclePutValidator>();
}