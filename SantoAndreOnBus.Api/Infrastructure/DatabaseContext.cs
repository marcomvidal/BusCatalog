using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Line> Lines { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
}
