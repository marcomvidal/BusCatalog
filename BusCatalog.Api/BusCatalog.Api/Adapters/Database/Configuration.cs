using Microsoft.EntityFrameworkCore;

namespace BusCatalog.Api.Adapters.Database;

public static class Configuration
{
    private const string ConnectionString = "Default";

    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DatabaseContext>(
            options => options.UseSqlite(
                builder.Configuration.GetConnectionString(ConnectionString)));

        return builder;
    }
}
