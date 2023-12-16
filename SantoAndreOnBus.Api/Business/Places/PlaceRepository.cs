using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Extensions;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Business.Places;

public interface IPlaceRepository
{
    Task<IEnumerable<Place>> GetAllAsync();
    Task<Place?> GetByIdAsync(int id);
    Task<Place?> GetByIdentificationAsync(string identification, int? id = null);
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

    public Task<Place?> GetByIdAsync(int id) =>
        _db.Places
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Place?> GetByIdentificationAsync(
        string identification,
        int? id = null) =>
        await _db.Places
            .AsNoTracking()
            .WhereIfTrue(id.HasValue, x => x.Id != id)
            .FirstOrDefaultAsync(x => x.Identification.ToLower().Equals(identification.ToLower()));

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
