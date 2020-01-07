using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Contexts;
using SantoAndreOnBus.Filters;
using SantoAndreOnBus.Models;

namespace SantoAndreOnBus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly DataContext _db;
        
        public VehiclesController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Vehicle>> Get()
        {
            return await _db.Vehicles.OrderBy(v => v.Id).ToListAsync();
        }

        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<Company>> Post([FromBody] Vehicle vehicle)
        {
            _db.Vehicles.Add(vehicle);
            await _db.SaveChangesAsync();

            return Ok(vehicle);
        }

        [ValidateModel]
        [HttpPut("{id}")]
        public async Task<ActionResult<Line>> Put(int id, [FromBody] Vehicle request)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            vehicle.Name = request.Name;
            _db.Entry(vehicle).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(vehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _db.Vehicles
                .Where(c => c.Id == id)
                .FirstAsync();

            _db.Vehicles.Remove(vehicle);
            await _db.SaveChangesAsync();

            return Ok($"Veículo {vehicle.Name} excluído.");
        }
    }
}