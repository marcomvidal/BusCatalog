using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Models;

namespace SantoAndreOnBus.Contexts
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Line> Lines { get; set; }
        public DbSet<InterestPoint> InterestPoints { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Prefix> Prefixes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<LineVehicle> LineVehicles { get; set; }
    }
}