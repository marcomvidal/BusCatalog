using Microsoft.EntityFrameworkCore;
using BusCatalog.Api.Adapters.Database;
using System.Linq.Expressions;

namespace BusCatalog.Api.Domain.Lines;

public interface ILineRepository
{
    Task<IEnumerable<Line>> GetAllAsync();

    Task<List<Line>> GetByAsync(
        Expression<Func<Line, bool>> predicate, int? quantity = null);

    Task<Line> SaveAsync(Line line);
    Task<int> UpdateAsync(Line line);
    Task<int> DeleteAsync(Line line);
}

public class LineRepository(DatabaseContext db) : ILineRepository
{
    private readonly DatabaseContext _db = db;

    public async Task<IEnumerable<Line>> GetAllAsync() => 
        await _db.Lines
            .AsNoTracking()
            .OrderBy(x => x.Identification)
            .ToListAsync();

    public async Task<List<Line>> GetByAsync(
        Expression<Func<Line, bool>> predicate,
        int? quantity = null) =>
        await _db.Lines
            .Where(predicate)
            .Include(x => x.Vehicles)
            .LimitIfHasQuantity(quantity)
            .ToListAsync();

    public async Task<Line> SaveAsync(Line line)
    {
        _db.Lines.Add(line);
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
