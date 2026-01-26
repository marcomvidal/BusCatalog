using Microsoft.EntityFrameworkCore;

namespace BusCatalog.Api.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void RemoveIfExists<T>()
        {
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(T));
            
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        public void EnsureDbCreated<T>() where T : DbContext
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<T>().Database;
            database.EnsureCreated();
        }
    }
}
