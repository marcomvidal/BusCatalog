using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Extensions;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle?> GetByIdentificationAsync(string identification, int? id = null);
    Task<int> SaveAsync(Vehicle vehicle);
    Task<int> UpdateAsync(Vehicle vehicle);
    Task<int> DeleteAsync(Vehicle vehicle);
}

public class VehicleRepository(DatabaseContext db) : IVehicleRepository
{
    private readonly DatabaseContext _db = db;

    public async Task<IEnumerable<Vehicle>> GetAllAsync() =>
        await _db.Vehicles
            .AsNoTracking()
            .OrderBy(x => x.Identification)
            .ToListAsync();

    public Task<Vehicle?> GetByIdAsync(int id) =>
        _db.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Vehicle?> GetByIdentificationAsync(
        string identification,
        int? id = null) =>
        await _db.Vehicles
            .AsNoTracking()
            .WhereIfTrue(id.HasValue, x => x.Id != id)
            .FirstOrDefaultAsync(x => x.Identification.ToLower().Equals(identification.ToLower()));

    public async Task<int> SaveAsync(Vehicle vehicle)
    {
        _db.Vehicles.Add(vehicle);
        
        return await _db.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(Vehicle vehicle)
    {
        _db.Entry(vehicle).State = EntityState.Modified;
        _db.Update(vehicle);

        return _db.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(Vehicle vehicle)
    {
        _db.Vehicles.Remove(vehicle);
        
        return _db.SaveChangesAsync();
    }
}
