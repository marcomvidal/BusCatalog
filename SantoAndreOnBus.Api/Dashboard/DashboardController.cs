using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SantoAndreOnBus.Api.Dashboard;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardRepository _repository;

    public DashboardController(IDashboardRepository repository)
        => _repository = repository;

    [HttpGet]
    public async Task<ActionResult<DashboardResponse>> GetAsync() =>
        Ok(await _repository.GetAsync());
}
