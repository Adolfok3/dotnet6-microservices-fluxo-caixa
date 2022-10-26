using Microsoft.AspNetCore.Builder;

namespace FluxoCaixa.Chassis.Swagger.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseSwagger(this WebApplication app, bool isDevelopment)
        {
            if (!isDevelopment)
                return;

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
