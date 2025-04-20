using System.Net;
using BusCatalog.Api.Domain.HealthCheck.Ports;
using static BusCatalog.Api.Domain.HealthCheck.Messages.ServiceMessages;

namespace BusCatalog.Api.Domain.HealthCheck;

public interface IHealthCheckAdapter
{
    Task<HealthCheckResponse> SendAsync(string url, string application);
}

public sealed class HealthCheckAdapter : IHealthCheckAdapter
{
    private readonly HttpClient _http;
    private readonly ILogger<HealthCheckAdapter> _logger;
    private readonly static int InvalidStatusCode = (int)HttpStatusCode.ServiceUnavailable;

    public HealthCheckAdapter(
        HttpClient http,
        ILogger<HealthCheckAdapter> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<HealthCheckResponse> SendAsync(string url, string application)
    {
        try
        {
            return await HandleSuccess(url, application);
        }
        catch (HttpRequestException)
        {
            return HandleError(url, application);
        }
    }

    private async Task<HealthCheckResponse> HandleSuccess(string url, string application)
    {
        var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        _logger.LogInformation(HealthCheckResult, application, url, (int)response.StatusCode);

        return HealthCheckResponse.Generate(
            url,
            (int)response.StatusCode,
            response.IsSuccessStatusCode);
    }

    private HealthCheckResponse HandleError(string url, string application)
    {
        _logger.LogWarning(HealthCheckResult, application, url, InvalidStatusCode);

        return HealthCheckResponse.Generate(
            url,
            InvalidStatusCode,
            success: false);
    }
}
