using System.Text;
using System.Text.Json;
using BusCatalog.Api.Domain.Lines.Ports;
using BusCatalog.Api.Infrastructure.Configurations;
using Confluent.Kafka;

namespace BusCatalog.Api.Domain.Lines;

public sealed class LineDeserializer : IDeserializer<LinePostRequest>
{
    public LinePostRequest Deserialize(
        ReadOnlySpan<byte> data,
        bool isNull,
        SerializationContext context) =>
        JsonSerializer.Deserialize<LinePostRequest>(
            Encoding.UTF8.GetString(data), Serialization.Options)!;
}