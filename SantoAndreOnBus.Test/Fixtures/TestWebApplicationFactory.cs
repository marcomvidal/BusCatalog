using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SantoAndreOnBus.Api.Extensions;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Test.Fixtures;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string ConnectionString = "Data Source=:memory:";
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

        builder.UseEnvironment("Development");
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