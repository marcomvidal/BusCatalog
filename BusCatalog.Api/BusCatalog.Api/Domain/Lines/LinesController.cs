using BusCatalog.Api.Domain.Lines.Messages;
using BusCatalog.Api.Domain.Lines.Ports;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BusCatalog.Api.Domain.Lines;

[Route("api/[controller]")]
[ApiController]
public sealed class LinesController : ControllerBase
{
    private readonly ILineService _service;
    private readonly IValidator<LinePostRequest> _postValidator;
    private readonly IValidator<LinePutRequest> _putValidator;

    public LinesController(
        ILineService service,
        IValidator<LinePostRequest> postValidator,
        IValidator<LinePutRequest> putValidator)
    {
        _service = service;
        _postValidator = postValidator;
        _putValidator = putValidator;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Line>>(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = EndpointMessages.GetLines)]
    public async Task<ActionResult<IEnumerable<Line>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{identification}")]
    [ProducesResponseType<Line>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = EndpointMessages.GetLine)]
    public async Task<ActionResult<Line>> Get(string identification)
    {
        var line = await _service.GetByIdentificationAsync(identification);

        return line is not null
            ? Ok(line)
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType<Line>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = EndpointMessages.PostLine)]
    public async Task<ActionResult<Line>> Post([FromBody] LinePostRequest request)
    {
        var validation = await _postValidator.ValidateModelAsync(request, ModelState);

        return validation.IsValid
            ? Accepted(await _service.SaveAsync(request))
            : ValidationProblem();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<Line>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = EndpointMessages.PutLine)]
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
    [ProducesResponseType<Line>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = EndpointMessages.DeleteLine)]
    public async Task<ActionResult<Line>> Delete(int id)
    {
        var line = await _service.GetByIdAsync(id);

        return line is not null
            ? Ok(await _service.DeleteAsync(line))
            : NotFound();
    }
}
