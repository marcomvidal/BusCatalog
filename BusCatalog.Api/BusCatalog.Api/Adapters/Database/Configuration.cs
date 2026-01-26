using Microsoft.EntityFrameworkCore;

namespace BusCatalog.Api.Adapters.Database;

public static class Configuration
{
    private const string ConnectionString = "Default";

    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder AddDatabase()
        {
            builder.Services.AddDbContext<DatabaseContext>(
                options => options.UseSqlite(
                    builder.Configuration.GetConnectionString(ConnectionString)));

            return builder;
        }
    }
}
