using Microsoft.EntityFrameworkCore;

namespace SantoAndreOnBus.Api.Infrastructure.Dependencies;

public static class Persistence
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString) =>
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(connectionString));
}
