using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Domain.General;

namespace SantoAndreOnBus.Api.Domain.Places;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class PlacesController(
    IPlaceValidator validator,
    IPlaceService service) : ControllerBase
{
    private readonly IPlaceValidator _validator = validator;
    private readonly IPlaceService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> Get() =>
        Ok(await _service.GetAllAsync());
    
    [HttpPost]
    public async Task<ActionResult<Place>> Post([FromBody] PlacePostRequest request)
    {
        var validation = await _validator.ValidateAsync(request, ModelState);
        
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

        var validation = await _validator.ValidateAsync(id, request, ModelState);

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
