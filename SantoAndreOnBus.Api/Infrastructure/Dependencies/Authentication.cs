using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SantoAndreOnBus.Api.Authentication;
using SantoAndreOnBus.Api.Infrastructure.Sections;

namespace SantoAndreOnBus.Api.Infrastructure;

public static class Authentication
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

        var authentication = builder.Configuration
            .GetSection("Authentication")
            .Get<AuthenticationSection>()!;
        
        builder.Services
            .AddAuthentication(AuthenticationConfig)
            .AddJwtBearer(b => BearerOptions(b, authentication));
            
        return builder;
    }

    private static Action<AuthenticationOptions> AuthenticationConfig = a => {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    };

    private static JwtBearerOptions BearerOptions(
        JwtBearerOptions options,
        AuthenticationSection authentication)
    {
        var key = Encoding.ASCII.GetBytes(authentication.Secret!);

        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidAudience = authentication.Audience,
            ValidIssuer = authentication.Issuer,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true
        };

        return options;
    }
}