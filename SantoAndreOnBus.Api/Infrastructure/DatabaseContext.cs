using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Business.Lines;
using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Api.Business.Vehicles;

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
}
