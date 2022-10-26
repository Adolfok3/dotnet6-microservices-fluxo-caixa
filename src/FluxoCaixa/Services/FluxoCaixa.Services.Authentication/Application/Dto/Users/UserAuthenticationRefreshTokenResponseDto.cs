namespace FluxoCaixa.Services.Authentication.Application.Dto.Users
{
    public class UserAuthenticationRefreshTokenResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
