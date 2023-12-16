using FluentValidation;

namespace SantoAndreOnBus.Api.Business.Lines;

public static class Configuration
{
    public static IServiceCollection AddLines(this IServiceCollection services) =>
        services
            .AddScoped<ILineRepository, LineRepository>()
            .AddScoped<ILineService, LineService>()
            .AddScoped<IValidator<LinePostRequest>, LinePostValidator>()
            .AddScoped<IValidator<LinePutRequest>, LinePutValidator>()
            .AddScoped<ILineValidator, LineValidator>();
}
