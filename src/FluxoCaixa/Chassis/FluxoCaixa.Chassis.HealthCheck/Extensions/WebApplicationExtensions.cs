using Microsoft.AspNetCore.Builder;

namespace FluxoCaixa.Chassis.HealthCheck.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseHealthChecks(this WebApplication app)
        {
            app.UseHealthChecks("/health");
        }
    }
}
