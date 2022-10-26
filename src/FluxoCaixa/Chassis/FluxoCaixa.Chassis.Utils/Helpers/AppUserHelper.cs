using FluxoCaixa.Chassis.Authentication.Utils.Interfaces;
using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FluxoCaixa.Chassis.Utils.Common;

namespace FluxoCaixa.Chassis.Utils.Helpers
{
    public class AppUserHelper : IAppUserHelper
    {
        private readonly IDistributedCache _cache;
        private readonly IJwtGenerator _jwtGenerator;

        public AppUserHelper(IDistributedCache cache, IJwtGenerator jwtGenerator)
        {
            _cache = cache;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AppUser> GetUserFromAccessTokenasync(string accessToken)
        {
            var user = await GetUserFromCacheAsync(accessToken);
            if (user != null)
                return user;

            var claims = _jwtGenerator.GetClaimsFromToken(accessToken);
            user = new AppUser
            {
                UserId = Guid.Parse(claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier)).Value),
                FullName = claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.GivenName)).Value
            };

            await SetUserToCache(user, accessToken);
            return user;
        }

        private async Task<AppUser> GetUserFromCacheAsync(string accessToken)
        {
            var data = await _cache.GetAsync($"user_access_token_cache_{accessToken}");
            return data == null ? null : JsonSerializer.Deserialize<AppUser>(Encoding.UTF8.GetString(data));
        }

        private async Task SetUserToCache(AppUser user, string accessToken)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
            await _cache.SetAsync($"user_access_token_cache_{accessToken}", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user)), options);
        }
    }
}
