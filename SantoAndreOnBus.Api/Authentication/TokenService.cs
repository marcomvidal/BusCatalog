using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SantoAndreOnBus.Api.Infrastructure.Configurations.Sections;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SantoAndreOnBus.Api.Authentication;

public interface ITokenService
{
    Task<string> Generate(string email);
}

public class TokenService : ITokenService
{
    private readonly AuthenticationSection _settings;
    private readonly UserManager<IdentityUser> _userManager;

    public TokenService(
        IOptions<AuthenticationSection> settings,
        UserManager<IdentityUser> userManager) 
    {
        _settings = settings.Value;
        _userManager = userManager;
    }

    public async Task<string> Generate(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var handler = new JwtSecurityTokenHandler();

        var secret = Environment.GetEnvironmentVariable("ASPNETCORE_JwtSecret") != null ?
            Environment.GetEnvironmentVariable("ASPNETCORE_JwtSecret") :
            _settings.Secret;
            
        var key = Encoding.ASCII.GetBytes(secret);

        var descriptor = new SecurityTokenDescriptor()
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Expires = DateTime.UtcNow.AddHours(Double.Parse(_settings.ExpirationInHours)),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        return handler.WriteToken(handler.CreateToken(descriptor));
    }
}
