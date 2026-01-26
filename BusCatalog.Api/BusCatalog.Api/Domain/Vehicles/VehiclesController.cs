using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.General;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Annotations;
using BusCatalog.Api.Domain.Vehicles.Ports;
using BusCatalog.Api.Domain.Vehicles.Messages;

namespace BusCatalog.Api.Domain.Vehicles;

[Route("api/[controller]")]
[ApiController]
public sealed class VehiclesController : ControllerBase
{
    private readonly IVehicleService _service;
    private readonly IValidator<VehiclePostRequest> _postValidator;
    private readonly IValidator<VehiclePutRequest> _putValidator;

    public VehiclesController(
        IVehicleService service,
        IValidator<VehiclePostRequest> postValidator,
        IValidator<VehiclePutRequest> putValidator)
    {
        _service = service;
        _postValidator = postValidator;
        _putValidator = putValidator;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Vehicle>>(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = EndpointMessages.GetVehicles)]
    public async Task<ActionResult<IEnumerable<Vehicle>>> Get() =>
        Ok(await _service.GetAllAsync());
    
    [HttpGet("{identification}")]
    [ProducesResponseType<Vehicle>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = EndpointMessages.GetVehicle)]
    public async Task<ActionResult<Vehicle>> GetByIdentification(string identification)
    {
        var vehicle = await _service.GetByIdentificationAsync(identification);

        return vehicle is not null ? Ok(vehicle) : NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType<Vehicle>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = EndpointMessages.PostVehicle)]
    public async Task<ActionResult<Vehicle>> Post([FromBody] VehiclePostRequest request)
    {
        var validation = await _postValidator.ValidateModelAsync(request, ModelState);

        return validation.IsValid
            ? Accepted(await _service.SaveAsync(request))
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<Vehicle>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = EndpointMessages.PutVehicle)]
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
    [ProducesResponseType<DeleteResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = EndpointMessages.DeleteVehicle)]
    public async Task<ActionResult<DeleteResponse>> Delete(int id)
    {
        var vehicle = await _service.GetByIdAsync(id);
        
        return vehicle is not null
            ? Ok(await _service.DeleteAsync(vehicle))
            : NotFound();
    }
}
