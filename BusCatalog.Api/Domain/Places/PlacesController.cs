using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BusCatalog.Api.Domain.General;
using BusCatalog.Api.Extensions;

namespace BusCatalog.Api.Domain.Places;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class PlacesController(
    IPlaceService service,
    IValidator<PlacePostRequest> postValidator,
    IValidator<PlacePutRequest> putValidator) : ControllerBase
{
    private readonly IPlaceService _service = service;
    private readonly IValidator<PlacePostRequest> _postValidator = postValidator;
    private readonly IValidator<PlacePutRequest> _putValidator = putValidator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> Get() =>
        Ok(await _service.GetAllAsync());
    
    [HttpPost]
    public async Task<ActionResult<Place>> Post([FromBody] PlacePostRequest request)
    {
        var validation = await _postValidator.ValidateModelAsync(request, ModelState);
        
        return validation.IsValid
            ? Accepted(await _service.SaveAsync(request))
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Place>> Put(int id, [FromBody] PlacePutRequest request)
    {
        var vehicle = await _service.GetByIdAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        var validation = await _putValidator.ValidateModelAsync(
            request with { Id = id },
            ModelState);

        return validation.IsValid
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
