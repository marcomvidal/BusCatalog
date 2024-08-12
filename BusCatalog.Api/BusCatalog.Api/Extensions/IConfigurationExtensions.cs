using BusCatalog.Api.Infrastructure.Configurations;

namespace BusCatalog.Api.Extensions;

public static class IConfigurationExtensions
{
    public static T GetValue<T>(this IConfiguration configuration, ConfigurationKeys key) =>
        configuration.GetValue<T>(key.ToString())!;
}