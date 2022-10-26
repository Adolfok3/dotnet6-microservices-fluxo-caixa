using Microsoft.AspNetCore.Http;

namespace FluxoCaixa.Chassis.Utils.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetAuthorizationHeader(this HttpRequest request)
        {
            var token = request.Headers.Authorization.ToString()?.Split(" ").Last();
            if (token == null)
                throw new UnauthorizedAccessException("Access token is required in Authorization header.");

            return token;
        }
    }
}
