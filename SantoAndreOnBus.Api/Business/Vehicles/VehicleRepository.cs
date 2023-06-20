using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<int> SaveAsync(Vehicle vehicle);
    Task<int> UpdateAsync(Vehicle vehicle);
    Task<int> DeleteAsync(Vehicle vehicle);
}

public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _db;

    public VehicleRepository(DatabaseContext db) => _db = db;

    public async Task<IEnumerable<Vehicle>> GetAllAsync() =>
        await _db.Vehicles
            .OrderBy(x => x.Identification)
            .ToArrayAsync();

    public Task<Vehicle?> GetByIdAsync(int id) =>
        _db.Vehicles.FirstOrDefaultAsync(x => x.Id == id); 

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
