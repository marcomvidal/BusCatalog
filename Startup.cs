using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SantoAndreOnBus.Contexts;
using SantoAndreOnBus.Helpers;
using SantoAndreOnBus.Models.Mappers;
using SantoAndreOnBus.Services;

namespace SantoAndreOnBus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private StartupUtilities _utilities = new StartupUtilities();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(options => 
                options.UseNpgsql(_utilities.GetConnectionString(Configuration)));

            // ASP.NET Core Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            // JSON Web Token
            var jwtConfiguration = new JwtConfiguration(services);
            jwtConfiguration.Configure(Configuration.GetSection("AppSettings"));

            services.AddScoped<LineService>();
            services.AddScoped<CompaniesService>();

            // AutoMapper
            var mapper = new MapperConfiguration(config => 
                config.AddProfile<LineModelToDTO>()
            );

            services.AddCors(_utilities.SetCorsPolicy);
            services.AddSingleton(mapper.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _utilities.MigrateDatabase(app);
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
