using FluxoCaixa.Chassis.HttpLogging.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FluxoCaixa.Chassis.HttpLogging.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseHttpLogginMiddleware(this WebApplication app)
        {
            app.UseMiddleware<HttpLoggingMiddleware>();
        }
    }
}
