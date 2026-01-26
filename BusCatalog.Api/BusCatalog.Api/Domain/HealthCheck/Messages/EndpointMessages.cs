namespace BusCatalog.Api.Domain.HealthCheck.Messages;

public static class EndpointMessages
{
    public const string GetHealthCheck = "Responds a health check request.";
    public const string SpaHealthCheck = "Sends a HTTP request against BusCatalog.Spa.";
    public const string ScraperHealthCheck = "Sends a HTTP request against BusCatalog.Scraper.";
}