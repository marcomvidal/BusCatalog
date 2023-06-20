using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.General;
using SantoAndreOnBus.Api.Infrastructure.Filters;
using SantoAndreOnBus.Api.Business.Lines;

namespace SantoAndreOnBus.Api.Business.Vehicles;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _repository;
    private readonly IVehicleService _service;
    
    public VehiclesController(IVehicleRepository repository, IVehicleService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<VehicleListResponse>> Get() =>
        Ok(await _service.GetAllAsync());
    
    [ValidateModel]
    [HttpPost]
    public async Task<ActionResult<VehicleResponse>> Post([FromBody] VehicleSubmitRequest request) =>
        Ok(await _service.SaveAsync(request));

    [ValidateModel]
    [HttpPut("{id}")]
    public async Task<ActionResult<Line>> Put(int id, [FromBody] VehicleSubmitRequest request)
    {
        var vehicle = await _service.GetByIdAsync(id);

        return vehicle.Data is not null
            ? Ok(await _service.UpdateAsync(request, vehicle.Data))
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteResponse>> Delete(int id)
    {
        var vehicle = await _service.GetByIdAsync(id);
        
        return vehicle.Data is not null
            ? Ok(await _service.DeleteAsync(vehicle.Data))
            : NotFound();
    }
}
