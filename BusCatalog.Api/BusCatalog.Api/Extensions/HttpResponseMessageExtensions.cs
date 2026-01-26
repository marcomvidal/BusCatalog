using System.Text.Json;
using BusCatalog.Api.Infrastructure.Configurations;

namespace BusCatalog.Api.Extensions;

public static class HttpResponseMessageExtensions
{
    extension(HttpResponseMessage response)
    {
        public async Task<T?> DeserializedBody<T>() =>
            await JsonSerializer.DeserializeAsync<T>(
                await response.Content.ReadAsStreamAsync(),
                Serialization.Options);
    }
}
