using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Contexts;
using SantoAndreOnBus.Filters;
using SantoAndreOnBus.Models;
using SantoAndreOnBus.Models.DTOs;
using SantoAndreOnBus.Services;

namespace SantoAndreOnBus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly CompaniesService _service;
        
        public CompaniesController(DataContext db, CompaniesService service)
        {
            _db = db;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await _db.Companies.Include(c => c.Prefixes).ToListAsync();
        }

        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<Company>> Post([FromBody] CompanyDTO request)
        {
            var company = new Company
            {
                Name = request.Name,
                Prefixes = request.Prefixes.Select(p => new Prefix{ Number = p.Number }).ToList()
            };

            _db.Companies.Add(company);
            await _db.SaveChangesAsync();

            return Ok(company);
        }

        [ValidateModel]
        [HttpPut("{id}")]
        public async Task<ActionResult<Company>> Put(int id, [FromBody] CompanyDTO request)
        {
            var company = await _service.GetByIdAsync(id);
            await _service.DeletePrefixesByCompanyAsync(company);

            company.Name = request.Name;
            company.Prefixes = request.Prefixes.Select(p => new Prefix{ Number = p.Number }).ToList();

            _db.Entry(company).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _service.GetByIdAsync(id);
            await _service.DeletePrefixesByCompanyAsync(company);
            _db.Companies.Remove(company);
            await _db.SaveChangesAsync();

            return Ok($"Empresa {company.Name} excluída com sucesso.");
        }
    }
}