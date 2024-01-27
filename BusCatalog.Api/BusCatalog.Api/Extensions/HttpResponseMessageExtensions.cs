using System.Text.Json;

namespace BusCatalog.Api.Extensions;

public static class HttpResponseMessageExtensions
{
    public async static Task<T?> DeserializedBody<T>(this HttpResponseMessage response) =>
        await JsonSerializer.DeserializeAsync<T>(
            await response.Content.ReadAsStreamAsync(),
            SerializerOptions);

    private static readonly JsonSerializerOptions SerializerOptions = 
        new() { PropertyNameCaseInsensitive = true };
}
