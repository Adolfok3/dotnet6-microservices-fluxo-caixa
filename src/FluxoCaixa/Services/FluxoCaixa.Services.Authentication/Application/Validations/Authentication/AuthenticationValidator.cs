using FluentValidation;
using FluxoCaixa.Chassis.Utils.Extensions;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Interfaces.Services;

namespace FluxoCaixa.Services.Authentication.Application.Validations.Authentication
{
    public class AuthenticationValidator<T> : IAuthenticationService where T : IAuthenticationService
    {
        private readonly T _inner;
        private readonly IValidator<UserAuthenticationRequestDto> _userAuthenticationRequestDtoValidator;

        public AuthenticationValidator(T inner, IValidator<UserAuthenticationRequestDto> userAuthenticationRequestDtoValidator)
        {
            _inner = inner;
            _userAuthenticationRequestDtoValidator = userAuthenticationRequestDtoValidator;
        }

        public async Task<UserAuthenticationResponseDto> LoginAsync(UserAuthenticationRequestDto dto)
        {
            await _userAuthenticationRequestDtoValidator.ValidateDtoAsync(dto);
            return await _inner.LoginAsync(dto);
        }

        public async Task<UserAuthenticationRefreshTokenResponseDto> RefreshTokenAsync(string token, string refreshTokenProvide)
        {
            return await _inner.RefreshTokenAsync(token, refreshTokenProvide);
        }
    }
}
