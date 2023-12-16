using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;
using SantoAndreOnBus.Api.Extensions;

namespace SantoAndreOnBus.Api.Business.Lines;

public interface ILineRepository
{
    Task<IEnumerable<Line>> GetAllAsync();
    Task<Line?> GetByIdAsync(int id);
    Task<Line?> GetByIdentificationAsync(string identification, int? id = null);
    Task<Line> SaveAsync(Line line);
    Task<int> UpdateAsync(Line line);
    Task<int> DeleteAsync(Line line);
}

public class LineRepository(DatabaseContext db) : ILineRepository
{
    private readonly DatabaseContext _db = db;

    public async Task<IEnumerable<Line>> GetAllAsync() => 
        await _db.Lines!
            .AsNoTracking()
            .OrderBy(x => x.Identification)
            .ToListAsync();

    public Task<Line?> GetByIdAsync(int id)
    {        
        return _db.Lines
            .AsNoTracking()
            .Include(x => x.Places)
            .Include(x => x.Vehicles)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Line?> GetByIdentificationAsync(
        string identification,
        int? id = null) =>
        await _db.Lines
            .AsNoTracking()
            .WhereIfTrue(id.HasValue, x => x.Id != id)
            .FirstOrDefaultAsync(
                x => x.Identification.ToLower().Equals(identification.ToLower()));

    public async Task<Line> SaveAsync(Line line)
    {
        _db.Lines!.Add(line);
        await _db.SaveChangesAsync();
        
        return line;
    }

    public async Task<int> UpdateAsync(Line line)
    {
        _db.Entry(line).State = EntityState.Modified;
        _db.Update(line);

        return await _db.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(Line line)
    {
        _db.Lines.Remove(line);
        
        return _db.SaveChangesAsync();
    }
}
