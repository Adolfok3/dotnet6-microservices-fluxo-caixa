using AutoMapper;
using FluxoCaixa.Chassis.Authentication.Utils.Interfaces;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Interfaces.Services;
using FluxoCaixa.Services.Authentication.Domain.Entities;
using FluxoCaixa.Services.Authentication.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text;

namespace FluxoCaixa.Services.Authentication.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserRepository repository, IJwtGenerator jwtGenerator, IDistributedCache cache, IMapper mapper)
        {
            _repository = repository;
            _jwtGenerator = jwtGenerator;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<UserAuthenticationResponseDto> LoginAsync(UserAuthenticationRequestDto dto)
        {
            var user = await _repository.GetUserByUsernameAsync(dto.Username);
            if (user == null)
                throw new ArgumentException("Invalid username or password.");

            ValidatePassword(user, dto.Password);

            var (token, expirationInMinutes) = _jwtGenerator.GenerateToken(user.Id, user.Fullname, user.Role);
            var (refreshToken, refreshTokenExpirationInMinutes) = _jwtGenerator.GenerateRefreshToken();

            await SetRefreshTokenToCacheAsync(user.Id.ToString(), refreshToken, refreshTokenExpirationInMinutes);

            var result = _mapper.Map<UserAuthenticationResponseDto>(user);
            result.AccessToken = token;
            result.RefreshToken = refreshToken;
            result.AccessTokenExpirationDate = DateTime.UtcNow.AddMinutes(expirationInMinutes);
            result.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(refreshTokenExpirationInMinutes);

            return result;
        }

        public async Task<UserAuthenticationRefreshTokenResponseDto> RefreshTokenAsync(string token, string refreshTokenProvide)
        {
            var claims = _jwtGenerator.GetClaimsFromToken(token);
            var userId = claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            var refreshToken = await GetRefreshTokenFromCacheAsync(userId);
            if (refreshToken != refreshTokenProvide)
                throw new ArgumentException("Invalid refresh token.");

            (token, var expirationInMinutes) = _jwtGenerator.GenerateToken(claims);
            (refreshToken, var refreshTokenExpirationInMinutes) = _jwtGenerator.GenerateRefreshToken();
            await SetRefreshTokenToCacheAsync(userId, refreshToken, refreshTokenExpirationInMinutes);

            return new UserAuthenticationRefreshTokenResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                AccessTokenExpirationDate = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(refreshTokenExpirationInMinutes),
            };
        }

        private async Task<string> GetRefreshTokenFromCacheAsync(string userId)
        {
            var data = await _cache.GetAsync($"user_refresh_token_cache_{userId}");
            return data == null ? null : Encoding.UTF8.GetString(data);
        }

        private async Task SetRefreshTokenToCacheAsync(string userId, string refreshToken, int refreshTokenExpirationInMinutes)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(refreshTokenExpirationInMinutes));
            await _cache.SetAsync($"user_refresh_token_cache_{userId}", Encoding.UTF8.GetBytes(refreshToken), options);
        }

        private void ValidatePassword(User user, string password)
        {
            var hash = new PasswordHasher<User>();
            var result = hash.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed)
                throw new ArgumentException("Invalid username or password.");
        }
    }
}
