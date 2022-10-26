using System.Security.Claims;

namespace FluxoCaixa.Chassis.Authentication.Utils.Interfaces
{
    public interface IJwtGenerator
    {
        List<Claim> GetClaimsFromToken(string token);
        (string, int) GenerateToken(Guid userId, string fullname, string role);
        (string, int) GenerateToken(IEnumerable<Claim> claims);
        (string, int) GenerateRefreshToken();
    }
}
