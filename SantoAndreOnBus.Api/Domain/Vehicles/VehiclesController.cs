using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Domain.General;

namespace SantoAndreOnBus.Api.Domain.Vehicles;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class VehiclesController(
    IVehicleValidator validator,
    IVehicleService service) : ControllerBase
{
    private readonly IVehicleValidator _validator = validator;
    private readonly IVehicleService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> Get() =>
        Ok(await _service.GetAllAsync());
    
    [HttpPost]
    public async Task<ActionResult<Vehicle>> Post([FromBody] VehiclePostRequest request)
    {
        var validation = await _validator.ValidateAsync(request, ModelState);

        return validation.IsValid
            ? Accepted(await _service.SaveAsync(request))
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Vehicle>> Put(int id, [FromBody] VehiclePutRequest request)
    {
        var vehicle = await _service.GetByIdAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        var validator = await _validator.ValidateAsync(id, request, ModelState);

        return validator.IsValid
            ? Accepted(await _service.UpdateAsync(request, vehicle))
            : ValidationProblem();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteResponse>> Delete(int id)
    {
        var vehicle = await _service.GetByIdAsync(id);
        
        return vehicle is not null
            ? Ok(await _service.DeleteAsync(vehicle))
            : NotFound();
    }
}
