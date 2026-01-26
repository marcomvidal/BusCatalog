using BusCatalog.Api.Domain.Vehicles.Ports;
using BusCatalog.Api.Domain.Vehicles.Validators;
using FluentValidation;

namespace BusCatalog.Api.Domain.Vehicles;

public static class Configuration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddVehicles() =>
            services
                .AddScoped<IVehicleRepository, VehicleRepository>()
                .AddScoped<IVehicleService, VehicleService>()
                .AddScoped<IValidator<VehiclePostRequest>, VehiclePostValidator>()
                .AddScoped<IValidator<VehiclePutRequest>, VehiclePutValidator>();
    }
}