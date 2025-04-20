using System.Text.Json;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class Serialization
{
    public static readonly JsonSerializerOptions Options = 
        new() { PropertyNameCaseInsensitive = true };
}
