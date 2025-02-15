using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BusCatalog.Api.Adapters.Database;

namespace BusCatalog.Api.Domain.Vehicles;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    
    Task<List<Vehicle>> GetByAsync(
        Expression<Func<Vehicle, bool>> predicate,
        int? quantity = null);

    Task<List<Vehicle>> GetByIdentificatorsAsync(IEnumerable<string> identificators);
    Task<int> SaveAsync(Vehicle vehicle);
    Task<int> UpdateAsync(Vehicle vehicle);
    Task<int> DeleteAsync(Vehicle vehicle);
}

public sealed class VehicleRepository(DatabaseContext db) : IVehicleRepository
{
    private readonly DatabaseContext _db = db;

    public async Task<IEnumerable<Vehicle>> GetAllAsync() =>
        await _db.Vehicles
            .AsNoTracking()
            .OrderBy(x => x.Identification)
            .ToListAsync();

    public Task<List<Vehicle>> GetByAsync(
        Expression<Func<Vehicle, bool>> predicate,
        int? quantity = null) =>
        _db.Vehicles
            .Where(predicate)
            .LimitIfHasQuantity(quantity)
            .OrderBy(x => x.Identification)
            .ToListAsync();

    public Task<List<Vehicle>> GetByIdentificatorsAsync(IEnumerable<string> identificators) =>
        GetByAsync(x => identificators.Contains(x.Identification));

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
