using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SantoAndreOnBus.Api.Infrastructure;

namespace SantoAndreOnBus.Test.Fixtures;

public class IntegrationTest : IDisposable
{
    public const string Environment = "Testing";
    private const string ConnectionString = "DataSource=:memory:";
    public SqliteConnection? Connection { get; private set; }
    public TestServer TestServer { get; }
    public HttpClient Client { get; }
    public IServiceProvider ServiceProvider { get; }
    public DatabaseContext Context { get; }

    private readonly static IEnumerable<Type> ServicesToReplace =
        [
            typeof(DbContextOptions<DatabaseContext>),
            typeof(DatabaseContext)
        ];

    public IntegrationTest()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment(Environment);
                builder.ConfigureServices(ConfigureServices);
            });
        
        TestServer = factory.Server;
        Client = factory.CreateClient();
        ServiceProvider = factory.Services;
        Context = ServiceProvider.GetService<DatabaseContext>()!;
    }

    private void ConfigureServices(IServiceCollection services)
    {
        foreach (var service in ServicesToReplace)
        {
            services.Remove(services.Single(x => x.ServiceType == service));
        }

        Connection = new SqliteConnection(ConnectionString);
        Connection.Open();
        services.AddDbContext<DatabaseContext>(x => x.UseSqlite(Connection));

        using var scope = services.BuildServiceProvider().CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Connection!.Close();
        ServiceProvider.GetRequiredService<DatabaseContext>().Dispose();
    }
}
