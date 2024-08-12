using BusCatalog.Api.Domain.Lines.Ports;
using BusCatalog.Api.Domain.Lines.Validators;
using FluentValidation;

namespace BusCatalog.Api.Domain.Lines;

public static class Configuration
{
    public static IServiceCollection AddLines(this IServiceCollection services) =>
        services
            .AddScoped<ILineRepository, LineRepository>()
            .AddScoped<ILineService, LineService>()
            .AddScoped<IValidator<LinePostRequest>, LinePostValidator>()
            .AddScoped<IValidator<LinePutRequest>, LinePutValidator>();
}
