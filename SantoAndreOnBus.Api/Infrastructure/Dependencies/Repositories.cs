using SantoAndreOnBus.Api.Companies;
using SantoAndreOnBus.Api.Lines;
using SantoAndreOnBus.Api.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure.Dependencies;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<ILineRepository, LineRepository>()
            .AddScoped<IVehicleRepository, VehicleRepository>();
}
