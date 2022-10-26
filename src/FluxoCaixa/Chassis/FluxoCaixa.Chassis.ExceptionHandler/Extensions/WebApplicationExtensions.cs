using FluxoCaixa.Chassis.ExceptionHandler.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FluxoCaixa.Chassis.ExceptionHandler.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseExceptionHandlerMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
