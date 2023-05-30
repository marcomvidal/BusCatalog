using SantoAndreOnBus.Api.Infrastructure.Configurations.Sections;

namespace SantoAndreOnBus.Api.Infrastructure.Configurations;

public static class ConfigurationConsolidator
{
    public static Consolidation Consolidate(this ConfigurationManager configuration)
    {
        return new Consolidation(
            configuration.GetConnectionString("Default"),
            configuration.GetSection("Authentication").Get<AuthenticationSection>());
    }    
}
