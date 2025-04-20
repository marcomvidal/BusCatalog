using BusCatalog.Api.Domain.HealthCheck.Ports;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static BusCatalog.Api.Domain.HealthCheck.Messages.EndpointMessages;

namespace BusCatalog.Api.Domain.HealthCheck;

[Route("api/[controller]")]
[ApiController]
public sealed class HealthCheckController : ControllerBase
{
    private readonly IHealthCheckService _service;

    public HealthCheckController(IHealthCheckService service) => _service = service;

    [HttpGet]
    [ProducesResponseType<HealthCheckResponse>(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = GetHealthCheck)]
    public ActionResult<HealthCheckResponse> Get() => Ok(_service.SelfHealthCheck());

    [HttpPost("spa")]
    [ProducesResponseType<HealthCheckResponse>(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = SpaHealthCheck)]
    public async Task<ActionResult<HealthCheckResponse>> Spa()
    {
        var response = await _service.SpaHealthCheckAsync();

        return response.ToResult();
    }

    [HttpPost("scraper")]
    [ProducesResponseType<HealthCheckResponse>(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = ScraperHealthCheck)]
    public async Task<ActionResult<HealthCheckResponse>> Scraper()
    {
        var response = await _service.ScraperHealthCheckAsync();

        return response.ToResult();
    }
}