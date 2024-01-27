using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using BusCatalog.Api.Adapters.Database;

namespace BusCatalog.Test.Fixtures;

public class IntegrationTest : IClassFixture<TestWebApplicationFactory>, IDisposable
{
    protected HttpClient Client { get; private set; }
    protected IServiceProvider ServiceProvider { get; private set; }
    protected DatabaseContext Context { get; private set; }
    private static readonly string[] TablesToClean =
    [
        "Vehicles",
        "Places",
        "Lines"
    ];

    public IntegrationTest(TestWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        var scope = factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        ServiceProvider = scope.ServiceProvider;
        Context = ServiceProvider.GetService<DatabaseContext>()!;
    }

    public void Dispose()
    {        
        foreach (var table in TablesToClean)
        {
            var statement = $"DELETE FROM {table}";
            Context.Database.ExecuteSqlRaw(statement);
        }

        ServiceProvider.GetRequiredService<DatabaseContext>().Dispose();
    }
}
