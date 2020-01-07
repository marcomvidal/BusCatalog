using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using SantoAndreOnBus.Contexts;

namespace SantoAndreOnBus.Helpers
{
    public class StartupUtilities
    {
        public string GetConnectionString(IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_PostgreSqlDatabase") != null ?
                Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_PostgreSqlDatabase") :
                configuration.GetConnectionString("PostgreSqlDatabase");
        }

        public void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.Migrate();
            }
        }

        public Action<CorsOptions> SetCorsPolicy = options => 
            options.AddPolicy("CorsPolicy", 
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
    }
}