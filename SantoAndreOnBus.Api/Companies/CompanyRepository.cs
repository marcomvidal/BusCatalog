using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Api.Companies;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<int> SaveAsync(Company company);
    Task<int> UpdateAsync(Company company);
    Task<int> DeleteAsync(Company company);
    Task<int> FlushPrefixesAsync(Company company);
}

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _db;
    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(DatabaseContext db, ILogger<CompanyRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Company>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered companies.");

        return await _db.Companies
            .Include(x => x.Prefixes)
            .ToListAsync();
    }
        

    public async Task<Company?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching company with ID {id}.", id);

        return await _db.Companies
            .Include(x => x.Prefixes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
        

    public async Task<int> SaveAsync(Company company)
    {
        _logger.LogInformation("Registering company {name}.", company.Name);
        await _db.Companies.AddAsync(company);
        var result = await _db.SaveChangesAsync();
        
        return result;
    }

    public async Task<int> UpdateAsync(Company company)
    {
        _logger.LogInformation("Updating company {name}.", company.Name);
        _db.Entry(company).State = EntityState.Modified;
        _db.Update(company);
        
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Company company)
    {
        _logger.LogInformation("Deleting company {name}.", company.Name);
        _db.Companies.Remove(company);

        return await _db.SaveChangesAsync();
    }

    public Task<int> FlushPrefixesAsync(Company company)
    {
        _logger.LogInformation("Flushing all prefixes of the company {name}", company.Name);
        
        if (company.Prefixes is null || !company.Prefixes.Any())
            return Task.FromResult(0);

        _db.Prefixes.RemoveRange(company.Prefixes);
        
        return _db.SaveChangesAsync();
    }
}
