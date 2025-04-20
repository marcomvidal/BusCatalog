using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BusCatalog.Api.Domain.HealthCheck.Ports;

public record HealthCheckResponse
{
    public string? Url { get; set;}
    public DateTime Time { get; set; }
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    
    public static HealthCheckResponse Generate(string url, int statusCode, bool success)
    {
        return new HealthCheckResponse
        {
            Time = DateTime.UtcNow,
            Url = url,
            StatusCode = statusCode,
            Success = success
        };
    }

    public ObjectResult ToResult()
    {
        return new ObjectResult(this)
        {
            StatusCode = Success ? StatusCode : (int)HttpStatusCode.BadGateway,
            ContentTypes = { "application/json" }
        };
    }
}
