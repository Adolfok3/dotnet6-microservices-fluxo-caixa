using FluxoCaixa.Services.Authentication.Application.Dto.Users;

namespace FluxoCaixa.Services.Authentication.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<UserAuthenticationResponseDto> LoginAsync(UserAuthenticationRequestDto dto);
        Task<UserAuthenticationRefreshTokenResponseDto> RefreshTokenAsync(string token, string refreshTokenProvide);
    }
}
