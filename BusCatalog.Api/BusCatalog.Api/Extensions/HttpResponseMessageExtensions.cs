using System.Text.Json;
using BusCatalog.Api.Infrastructure.Configurations;

namespace BusCatalog.Api.Extensions;

public static class HttpResponseMessageExtensions
{
    public async static Task<T?> DeserializedBody<T>(this HttpResponseMessage response) =>
        await JsonSerializer.DeserializeAsync<T>(
            await response.Content.ReadAsStreamAsync(),
            Serialization.Options);
}
