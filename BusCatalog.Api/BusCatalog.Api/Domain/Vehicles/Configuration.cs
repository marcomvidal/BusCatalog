using BusCatalog.Api.Domain.Vehicles.Ports;
using BusCatalog.Api.Domain.Vehicles.Validators;
using FluentValidation;

namespace BusCatalog.Api.Domain.Vehicles;

public static class Configuration
{
    public static IServiceCollection AddVehicles(this IServiceCollection services) =>
        services
            .AddScoped<IVehicleRepository, VehicleRepository>()
            .AddScoped<IVehicleService, VehicleService>()
            .AddScoped<IValidator<VehiclePostRequest>, VehiclePostValidator>()
            .AddScoped<IValidator<VehiclePutRequest>, VehiclePutValidator>();
}