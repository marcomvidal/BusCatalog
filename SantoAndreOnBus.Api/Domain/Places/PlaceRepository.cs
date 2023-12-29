using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Extensions;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Domain.Places;

public interface IPlaceRepository
{
    Task<IEnumerable<Place>> GetAllAsync();
    
    Task<List<Place>> GetByAsync(
        Expression<Func<Place, bool>> predicate,
        int? quantity = null);
    
    Task<int> SaveAsync(Place place);
    Task<int> UpdateAsync(Place place);
    Task<int> DeleteAsync(Place place);
}

public class PlaceRepository(DatabaseContext db) : IPlaceRepository
{
    private readonly DatabaseContext _db = db;

    public async Task<IEnumerable<Place>> GetAllAsync() =>
        await _db.Places
            .AsNoTracking()
            .OrderBy(x => x.Identification)
            .ToListAsync();

    public async Task<List<Place>> GetByAsync(
        Expression<Func<Place, bool>> predicate,
        int? quantity = null) =>
        await _db.Places
            .Where(predicate)
            .LimitIfHasQuantity(quantity)
            .ToListAsync();

    public async Task<int> SaveAsync(Place place)
    {
        _db.Places.Add(place);
        
        return await _db.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(Place place)
    {
        _db.Entry(place).State = EntityState.Modified;
        _db.Update(place);

        return _db.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(Place place)
    {
        _db.Places.Remove(place);
        
        return _db.SaveChangesAsync();
    }
}
