using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Contexts;
using SantoAndreOnBus.Models.DTOs;

namespace SantoAndreOnBus.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext _db;

        public DashboardController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<DashboardDTO> Get()
        {
            return new DashboardDTO
            {
                Companies = await _db.Companies.CountAsync(),
                Lines = await _db.Lines.CountAsync(),
                Vehicles = await _db.Vehicles.CountAsync()
            };
        }
    }
}