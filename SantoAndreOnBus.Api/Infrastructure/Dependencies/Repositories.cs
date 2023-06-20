using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure.Dependencies;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<ILineRepository, LineRepository>()
            .AddScoped<IVehicleRepository, VehicleRepository>();
}
