using BusCatalog.Api.Infrastructure.Configurations;

namespace BusCatalog.Api.Extensions;

public static class IConfigurationExtensions
{
    extension(IConfiguration configuration)
    {
        public T GetValue<T>(ConfigurationKeys key) =>
            configuration.GetValue<T>(key.ToString())!;
    }
}