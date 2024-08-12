using BusCatalog.Api.Domain.Lines.Ports;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BusCatalog.Api.Domain.Lines;

[Route("api/[controller]")]
[ApiController]
public class LinesController(
    ILineService service,
    IValidator<LinePostRequest> postValidator,
    IValidator<LinePutRequest> putValidator) : ControllerBase
{
    private readonly ILineService _service = service;
    private readonly IValidator<LinePostRequest> _postValidator = postValidator;
    private readonly IValidator<LinePutRequest> _putValidator = putValidator;

    [HttpGet]
    [SwaggerOperation(Summary = "Gets all registered lines.")]
    public async Task<ActionResult<IEnumerable<Line>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{identification}")]
    [SwaggerOperation(Summary = "Gets a line by its identification.")]
    public async Task<ActionResult<Line>> Get(string identification)
    {
        var line = await _service.GetByIdentificationAsync(identification);

        return line is not null
            ? Ok(line)
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Registers a new line.")]
    public async Task<ActionResult<Line>> Post([FromBody] LinePostRequest request)
    {
        var validation = await _postValidator.ValidateModelAsync(request, ModelState);

        return validation.IsValid
            ? Accepted(await _service.SaveAsync(request))
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Updates data of a previously registered line.")]
    public async Task<ActionResult<Line>> Put(int id, [FromBody] LinePutRequest request)
    {
        var line = await _service.GetByIdAsync(id);

        if (line is null)
        {
            return NotFound();
        }

        var validation = await _putValidator.ValidateModelAsync(
            request with { Id = id },
            ModelState);

        return validation.IsValid
            ? Accepted(await _service.UpdateAsync(request, line))
            : ValidationProblem();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletes a line by its ID.")]
    public async Task<ActionResult<Line>> Delete(int id)
    {
        var line = await _service.GetByIdAsync(id);

        return line is not null
            ? Ok(await _service.DeleteAsync(line))
            : NotFound();
    }
}
