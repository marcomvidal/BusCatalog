using Microsoft.EntityFrameworkCore;

namespace BusCatalog.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RemoveIfExists<T>(this IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(T));
        
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<T>().Database;
        database.EnsureCreated();
    }
}
