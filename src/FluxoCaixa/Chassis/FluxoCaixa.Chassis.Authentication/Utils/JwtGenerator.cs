using FluxoCaixa.Chassis.Authentication.Utils.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FluxoCaixa.Chassis.Authentication.Utils
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string _secret;
        private readonly int _expirationInMinutes;
        private readonly int _refreshTokenExpirationInMinutes;

        public JwtGenerator(IConfiguration configuration)
        {
            var section = configuration.GetSection("Jwt");
            _secret = section["Secret"];
            _expirationInMinutes = section.GetValue<int>("ExpirationInMinutes");
            _refreshTokenExpirationInMinutes = section.GetValue<int>("RefreshTokenExpirationInMinutes");
        }

        public (string, int) GenerateToken(Guid userId, string fullname, string role)
        {
            return GenerateToken(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.GivenName, fullname),
                new Claim(ClaimTypes.Role, role),
            });
        }

        public (string, int) GenerateToken(IEnumerable<Claim> claims)
        {
            var expiration = DateTime.UtcNow.AddMinutes(_expirationInMinutes);
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token), _expirationInMinutes);
        }

        public List<Claim> GetClaimsFromToken(string token)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret))
            };
            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, parameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Invalid JWT token.");

            return principal.Claims.ToList();
        }

        public (string, int) GenerateRefreshToken()
        {
            var random = $"{Guid.NewGuid():N}{Guid.NewGuid():N}";
            var bytes = Encoding.ASCII.GetBytes(random);
            return (Convert.ToBase64String(bytes), _refreshTokenExpirationInMinutes);
        }
    }
}
