using Microsoft.AspNetCore.Mvc;

namespace SantoAndreOnBus.Api.Business.Lines;

[Route("api/[controller]")]
[ApiController]
public class LinesController(
    ILineValidator validator,
    ILineService service) : ControllerBase
{
    private readonly ILineValidator _validator = validator;
    private readonly ILineService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Line>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{identification}")]
    public async Task<ActionResult<Line>> Get(string identification)
    {
        var line = await _service.GetByIdentificationAsync(identification);

        return line is not null
            ? Ok(line)
            : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Line>> Post([FromBody] LinePostRequest request)
    {
        var validation = await _validator.ValidateAsync(request, ModelState);
        var line = await _service.SaveAsync(request);

        return validation.IsValid
            ? Accepted(line)
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Line>> Put(int id, [FromBody] LinePostRequest request)
    {
        var line = await _service.GetByIdAsync(id);

        if (line is null)
        {
            return NotFound();
        }

        var validation = await _validator.ValidateAsync(request, ModelState);

        return validation.IsValid
            ? Accepted(await _service.UpdateAsync(request, line))
            : ValidationProblem();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Line>> Delete(int id)
    {
        var line = await _service.GetByIdAsync(id);

        return line is not null
            ? Ok(await _service.DeleteAsync(line))
            : NotFound();
    }
}
