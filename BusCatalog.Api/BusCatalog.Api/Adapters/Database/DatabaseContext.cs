using Microsoft.EntityFrameworkCore;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Adapters.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : DbContext(options)
{
    public DbSet<Line> Lines { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
}
