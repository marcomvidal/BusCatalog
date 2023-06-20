using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure.Dependencies;

public static class Services
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<ICompanyService, CompanyService>()
            .AddScoped<IVehicleService, VehicleService>();
}