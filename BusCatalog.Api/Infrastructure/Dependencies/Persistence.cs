using Microsoft.EntityFrameworkCore;

namespace BusCatalog.Api.Infrastructure;

public static class Persistence
{
    private const string ConnectionString = "Default";

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString(ConnectionString)));

        return builder;
    }
        
}
