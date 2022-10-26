using FluxoCaixa.Chassis.ExceptionHandler.Extensions;
using FluxoCaixa.Chassis.HealthCheck.Extensions;
using FluxoCaixa.Chassis.HttpLogging.Extensions;
using FluxoCaixa.Chassis.Metrics.Extensions;
using FluxoCaixa.Chassis.Persistence.Extensions;
using FluxoCaixa.Chassis.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Chassis.Startup.Extentions
{
    public static class WebApplicationExtensions
    {
        public static void UseDefaultSetup<T>(this WebApplication app, string environment) where T : DbContext
        {
            if (environment == "Testing")
            {
                app.UseAuthentication();
                app.UseAuthorization();
                return;
            }

            app.UseSwagger(environment == "Development");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHealthChecks();
            app.UseMetrics();

            app.UseHttpLogginMiddleware();
            app.UseExceptionHandlerMiddleware();

            app.MigrateDatabase<T>();
        }
    }
}
