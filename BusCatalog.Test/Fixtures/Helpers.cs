using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusCatalog.Test;

public static class Helpers
{
    private static readonly JsonSerializerOptions SerializationOptions = 
        new() { PropertyNameCaseInsensitive = true };

    public async static Task<T?> DeserializedBody<T>(this HttpResponseMessage response) =>
        await JsonSerializer.DeserializeAsync<T>(
            await response.Content.ReadAsStreamAsync(),
            SerializationOptions);
}
