using Confluent.Kafka;

namespace BusCatalog.Api.Domain.Lines.Configurations;

public record LinesConsumerSection
{
    public string Topic { get; init; } = string.Empty;
    public string BootstrapServers { get; init; } = string.Empty;

    public ConsumerConfig ToConsumerConfiguration()
    {
        return new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = Topic
        };
    }
}
