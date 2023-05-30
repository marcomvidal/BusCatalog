using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SantoAndreOnBus.Test;

public static class Helpers
{
    public async static Task<T?> DeserializedBody<T>(this HttpResponseMessage response) =>
        await JsonSerializer.DeserializeAsync<T>(
            await response.Content.ReadAsStreamAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
}
