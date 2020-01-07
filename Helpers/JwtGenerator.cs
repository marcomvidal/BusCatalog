using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace SantoAndreOnBus.Helpers
{
    public class JwtGenerator
    {
        private readonly AppSettings _settings;
        private readonly UserManager<IdentityUser> _userManager;

        public JwtGenerator(IOptions<AppSettings> settings,
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
}