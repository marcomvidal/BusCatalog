using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SantoAndreOnBus.Helpers
{
    public class JwtConfiguration
    {
        private IServiceCollection _services;

        public JwtConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void Configure(IConfigurationSection settingsSection)
        {
            var settings = settingsSection.Get<AppSettings>();

            _services.AddScoped<JwtGenerator>();
            _services.Configure<AppSettings>(settingsSection);
            _services.AddAuthentication(Authentication)
                .AddJwtBearer(b => BearerOptions(b, settings, GetJwtSecret(settings)));
        }

        public string GetJwtSecret(AppSettings settings)
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_JwtSecret") != null ?
                Environment.GetEnvironmentVariable("ASPNETCORE_JwtSecret") :
                settings.Secret;
        }

        private Action<AuthenticationOptions> Authentication = a => {
            a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        };

        private JwtBearerOptions BearerOptions(JwtBearerOptions b, AppSettings settings, string secret)
        {
            var key = Encoding.ASCII.GetBytes(secret);

            b.RequireHttpsMetadata = true;
            b.SaveToken = true;
            b.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidAudience = settings.Audience,
                ValidIssuer = settings.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true
            };

            return b;
        }
    }
}