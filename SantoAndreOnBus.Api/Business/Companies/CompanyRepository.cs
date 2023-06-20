using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Business.Companies;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<int> SaveAsync(Company company);
    Task<int> UpdateAsync(Company company);
    Task<int> DeleteAsync(Company company);
    Task<int> FlushPrefixesAsync(Company company);
}

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _db;

    public CompanyRepository(DatabaseContext db) => _db = db;

    public async Task<IEnumerable<Company>> GetAllAsync() =>
        await _db.Companies
            .Include(x => x.Prefixes)
            .OrderBy(x => x.Name)
            .ToArrayAsync();
        

    public async Task<Company?> GetByIdAsync(int id) =>
        await _db.Companies
            .Include(x => x.Prefixes)
            .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<int> SaveAsync(Company company)
    {
        await _db.Companies.AddAsync(company);

        return await _db.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(Company company)
    {
        _db.Entry(company).State = EntityState.Modified;
        _db.Update(company);
        
        return _db.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(Company company)
    {
        _db.Companies.Remove(company);

        return _db.SaveChangesAsync();
    }

    public Task<int> FlushPrefixesAsync(Company company)
    {        
        if (company.Prefixes is null || !company.Prefixes.Any())
            return Task.FromResult(0);

        _db.Prefixes.RemoveRange(company.Prefixes);
        
        return _db.SaveChangesAsync();
    }
}
