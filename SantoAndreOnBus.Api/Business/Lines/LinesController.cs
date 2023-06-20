using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Infrastructure.Filters;

namespace SantoAndreOnBus.Api.Business.Lines;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LinesController : ControllerBase
{
    private ILineRepository _repository;
    private readonly IMapper _mapper;
    
    public LinesController(ILineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Line>>> Get() =>
        Ok(await _repository.GetAllOrderedByNumber());

    [HttpGet("{lineName}")]
    public async Task<IActionResult> Get(string lineName)
    {
        try
        {
            var line = await _repository.GetByLineNameAsync(lineName);
            var response = _mapper.Map<LineSubmitRequest>(line);

            return Ok(response);
        }
        catch (InvalidOperationException)
        {
            return BadRequest($"Linha {lineName} não encontrada.");
        }
    }

    [ValidateModel]
    [HttpPost]
    public async Task<ActionResult<Line>> Post([FromBody] LineSubmitRequest request)
    {
        if (await _repository.GetCountByNameAsync(request) > 0)
            return BadRequest($"Linha {request.Letter}-{request.Number} já existe e não pode ser criada novamente.");

        var line = await _repository.SaveAsync(request);

        return Ok(line);
    }

    [ValidateModel]
    [HttpPut("{lineName}")]
    public async Task<ActionResult<Line>> Put(string lineName, [FromBody] LineSubmitRequest request)
    {
        var line = await _repository.GetByLineNameAsync(lineName);
        _repository.ClearRelationships(request, line);
        line = await _repository.UpdateAsync(line, request);

        return Ok(line);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Line>> Delete(int id)
    {
        await _repository.DeleteAsync(id);

        return Ok($"Linha excluída com sucesso.");
    }
}
