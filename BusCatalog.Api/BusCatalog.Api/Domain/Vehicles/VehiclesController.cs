using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.General;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BusCatalog.Api.Domain.Vehicles;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController(
    IVehicleService service,
    IValidator<VehiclePostRequest> postValidator,
    IValidator<VehiclePutRequest> putValidator) : ControllerBase
{
    private readonly IVehicleService _service = service;
    private readonly IValidator<VehiclePostRequest> _postValidator = postValidator;
    private readonly IValidator<VehiclePutRequest> _putValidator = putValidator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> Get() =>
        Ok(await _service.GetAllAsync());
    
    [HttpGet("{identification}")]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vehicle>> GetByIdentification(string identification)
    {
        var vehicle = await _service.GetByIdentificationAsync(identification);

        return vehicle is not null ? Ok(vehicle) : NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Vehicle>> Post([FromBody] VehiclePostRequest request)
    {
        var validation = await _postValidator.ValidateModelAsync(request, ModelState);

        return validation.IsValid
            ? Accepted(await _service.SaveAsync(request))
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Vehicle>> Put(int id, [FromBody] VehiclePutRequest request)
    {
        var vehicle = await _service.GetByIdAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        var validator = await _putValidator.ValidateModelAsync(
            request with { Id = id },
            ModelState);

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
