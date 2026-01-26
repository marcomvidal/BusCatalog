using BusCatalog.Api.Adapters.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusCatalog.Test.Fixtures;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string ConnectionString = "Data Source=:memory:";
    private const string TestEnvironment = "Development";
    private SqliteConnection _connection = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            InitializeDatabaseConnection();
            services.RemoveIfExists<DbContextOptions<DatabaseContext>>();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite(_connection));
            services.EnsureDbCreated<DatabaseContext>();
        });

        builder.UseEnvironment(TestEnvironment);
    }

    private void InitializeDatabaseConnection()
    {
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection.Close();
    }
}
