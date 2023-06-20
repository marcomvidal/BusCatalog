using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Business.Dashboard;

public interface IDashboardRepository
{
    Task<DashboardResponse> GetAsync();
}

public class DashboardRepository : IDashboardRepository
{
    private readonly DatabaseContext _db;

    public DashboardRepository(DatabaseContext db) => _db = db;
    
    public async Task<DashboardResponse> GetAsync() =>
        new DashboardResponse
        {
            Companies = await _db.Companies.CountAsync(),
            Lines = await _db.Lines.CountAsync(),
            Vehicles = await _db.Vehicles.CountAsync()
        };
}