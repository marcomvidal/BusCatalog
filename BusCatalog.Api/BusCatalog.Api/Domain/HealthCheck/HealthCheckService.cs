using System.Net;
using BusCatalog.Api.Domain.HealthCheck.Ports;
using BusCatalog.Api.Infrastructure.Configurations;

namespace BusCatalog.Api.Domain.HealthCheck;

public interface IHealthCheckService
{
    HealthCheckResponse SelfHealthCheck();
    Task<HealthCheckResponse> SpaHealthCheckAsync();
    Task<HealthCheckResponse> ScraperHealthCheckAsync();
}

public sealed class HealthCheckService : IHealthCheckService
{
    private readonly IHealthCheckAdapter _adapter;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public HealthCheckService(
        IHealthCheckAdapter adapter,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _adapter = adapter;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public HealthCheckResponse SelfHealthCheck() =>
        HealthCheckResponse.Generate(
            _httpContextAccessor.HttpContext!.GetFullRequestUrl(),
            (int)HttpStatusCode.OK,
            success: true);

    public Task<HealthCheckResponse> SpaHealthCheckAsync() =>
        _adapter.SendAsync(
            _configuration[nameof(ConfigurationKeys.SpaUrl)]!,
            nameof(HealthCheckApplication.Spa));

    public Task<HealthCheckResponse> ScraperHealthCheckAsync() =>
        _adapter.SendAsync(
            $"{_configuration[nameof(ConfigurationKeys.ScraperUrl)]}/api/health-check",
            nameof(HealthCheckApplication.Scraper));
}