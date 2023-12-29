using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Domain.Lines;
using SantoAndreOnBus.Api.Domain.Places;
using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Line> Lines { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
}
