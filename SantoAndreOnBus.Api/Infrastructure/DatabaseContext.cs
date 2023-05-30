using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Lines;
using SantoAndreOnBus.Api.Companies;
using SantoAndreOnBus.Api.Vehicles;

namespace SantoAndreOnBus.Api.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    
    public DbSet<Line> Lines { get; set; } = null!;
    public DbSet<InterestPoint> InterestPoints { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Prefix> Prefixes { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<LineVehicle> LineVehicles { get; set; } = null!;
}
