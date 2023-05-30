using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Companies;
using SantoAndreOnBus.Api.Infrastructure.Filters;
using SantoAndreOnBus.Api.Lines;

namespace SantoAndreOnBus.Api.Vehicles;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _repository;
    
    public VehiclesController(IVehicleRepository repository) =>
        _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> Get() =>
        Ok(await _repository.GetAllAsync());
    
    [ValidateModel]
    [HttpPost]
    public async Task<ActionResult<Company>> Post([FromBody] Vehicle vehicle)
    {
        await _repository.SaveAsync(vehicle);

        return Ok(vehicle);
    }

    [ValidateModel]
    [HttpPut("{id}")]
    public async Task<ActionResult<Line>> Put(int id, [FromBody] Vehicle request)
    {
        var vehicle = await _repository.GetAsync(id);

        if (vehicle is null)
            return BadRequest();

        vehicle.Name = request.Name;
        await _repository.UpdateAsync(vehicle);

        return Ok(vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _repository.GetAsync(id);

        if (vehicle is null)
            return BadRequest();

        await _repository.DeleteAsync(vehicle);
        
        return Ok(vehicle);
    }
}
