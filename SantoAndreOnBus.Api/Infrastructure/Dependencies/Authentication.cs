using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SantoAndreOnBus.Api.Authentication;
using SantoAndreOnBus.Api.Infrastructure.Configurations.Sections;

namespace SantoAndreOnBus.Api.Infrastructure.Dependencies;

public static class Authentication
{
    public static void AddJwtAuthentication(
        this IServiceCollection services,
        AuthenticationSection authentication)
    {
        services.AddScoped<ITokenService, TokenService>();

        services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        
        services.AddAuthentication(AuthenticationConfig)
            .AddJwtBearer(b => BearerOptions(b, authentication));
    }

    private static Action<AuthenticationOptions> AuthenticationConfig = a => {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    };

    private static JwtBearerOptions BearerOptions(
        JwtBearerOptions x,
        AuthenticationSection authentication)
    {
        var key = Encoding.ASCII.GetBytes(authentication.Secret!);

        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidAudience = authentication.Audience,
            ValidIssuer = authentication.Issuer,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true
        };

        return x;
    }
}