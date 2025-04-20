using BusCatalog.Api.Domain.Lines.Ports;
using BusCatalog.Api.Domain.Lines.Validators;
using BusCatalog.Api.Domain.Lines.Services;
using FluentValidation;
using Confluent.Kafka;
using BusCatalog.Api.Infrastructure.Configurations;

namespace BusCatalog.Api.Domain.Lines.Configurations;

public static class Configuration
{
    public static IServiceCollection AddLines(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddScoped<ILineRepository, LineRepository>()
            .AddScoped<ILineService, LineService>()
            .AddScoped<IValidator<LinePostRequest>, LinePostValidator>()
            .AddScoped<IValidator<LinePutRequest>, LinePutValidator>()
            .AddLineConsumerBackgroundService(configuration);

    private static IServiceCollection AddLineConsumerBackgroundService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(ConfigurationKeys.LinesConsumer));
        services.Configure<LinesConsumerSection>(section);
        var consumerConfiguration = section.Get<LinesConsumerSection>()!.ToConsumerConfiguration();
                
        services.AddSingleton(
            new ConsumerBuilder<Null, LinePostRequest>(consumerConfiguration)
                .SetValueDeserializer(new LineDeserializer())
                .Build());

        return services.AddHostedService<LineConsumerService>();
    }
}
