using FluxoCaixa.Chassis.ExceptionHandler.Models;
using FluxoCaixa.Chassis.Swagger.Extensions;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Extensions;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Interfaces.Services;

namespace FluxoCaixa.Services.Authentication.Controllers
{
    public class AuthenticationController
    {
        private const string Endpoint = "authentication";

        public static void MapEndpoints(WebApplication app)
        {
            app.MapPost($"{Endpoint}/login", async (IAuthenticationService service, UserAuthenticationRequestDto dto) =>
                {
                    var authentication = await service.LoginAsync(dto);
                    return AppResults.Ok(authentication);
                })
                .Produces(200, typeof(DefaultResponse<UserAuthenticationResponseDto>))
                .Produces(400, typeof(ErrorResponse))
                .WithDisplayName("Authentication")
                .WithDescription("Login")
                .AllowAnonymous();

            app.MapPost($"{Endpoint}/refresh-token", async (HttpRequest request, IAuthenticationService service, UserAuthenticationRefreshTokenRequestDto dto) =>
                {
                    var token = request.GetAuthorizationHeader();
                    var authentication = await service.RefreshTokenAsync(token, dto.RefreshToken);
                    return AppResults.Ok(authentication);
                })
                .Produces(200, typeof(DefaultResponse<UserAuthenticationRefreshTokenResponseDto>))
                .Produces(400, typeof(ErrorResponse))
                .WithDisplayName("Authentication")
                .WithDescription("Refresh token")
                .RequireAuthorization("user");

        }
    }
}
