using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantoAndreOnBus.Contexts;
using SantoAndreOnBus.Filters;
using SantoAndreOnBus.Models;
using SantoAndreOnBus.Services;
using SantoAndreOnBus.Models.DTOs;

namespace SantoAndreOnBus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LinesController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        private LineService _service;
        
        public LinesController(DataContext db, IMapper mapper, LineService service)
        {
            _db = db;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Line>> Get()
        {
            return await _db.Lines.OrderBy(l => l.Number).ToListAsync();
        }

        [HttpGet("{lineName}")]
        public async Task<IActionResult> Get(string lineName)
        {
            try
            {
                var line = await _service.GetByLineNameAsync(lineName);
                var response = _mapper.Map<LineDTO>(line);

                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                return BadRequest($"Linha {lineName} não encontrada.");
            }
        }

        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<Line>> Post([FromBody] LineDTO request)
        {
            if (await _service.GetCountByNameAsync(request) > 0)
            {
                return BadRequest($"Linha {request.Letter}-{request.Number} já existe e não pode ser criada novamente.");
            }

            var line = await _service.SaveAsync(request);

            return Ok(line);
        }

        [ValidateModel]
        [HttpPut("{lineName}")]
        public async Task<ActionResult<Line>> Put(string lineName, [FromBody] LineDTO request)
        {
            var line = await _service.GetByLineNameAsync(lineName);
            _service.ClearRelationships(request, line);
            line = await _service.UpdateAsync(line, request);

            return Ok(line);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Line>> Delete(int id)
        {
            var line = await _db.Lines.Where(l => l.Id == id).FirstAsync();
            _db.Lines.Remove(line);
            await _db.SaveChangesAsync();

            return Ok($"Linha {line.ToString()} excluída com sucesso.");
        }
    }
}