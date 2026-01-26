namespace BusCatalog.Api.Domain.HealthCheck.Messages;

public static class ServiceMessages
{
    public const string HealthCheckResult =
        "Sent HealthCheck to BusCatalog.{Application}: URL {Url}, StatusCode: {StatusCode}";
}