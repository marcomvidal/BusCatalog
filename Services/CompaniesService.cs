using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SantoAndreOnBus.Contexts;
using SantoAndreOnBus.Models;

namespace SantoAndreOnBus.Services
{
    public class CompaniesService
    {
        private readonly DataContext _db;

        public CompaniesService(DataContext db)
        {
            _db = db;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _db.Companies
                .Where(c => c.Id == id)
                .Include(c => c.Prefixes)
                .FirstAsync();
        }

        public async Task<int> DeletePrefixesByCompanyAsync(Company company)
        {
            _db.Prefixes.RemoveRange(company.Prefixes);
            return await _db.SaveChangesAsync();
        }
    }
}