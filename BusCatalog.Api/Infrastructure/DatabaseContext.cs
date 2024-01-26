using Microsoft.EntityFrameworkCore;
using BusCatalog.Api.Domain.Lines;
using BusCatalog.Api.Domain.Places;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Line> Lines { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
}
