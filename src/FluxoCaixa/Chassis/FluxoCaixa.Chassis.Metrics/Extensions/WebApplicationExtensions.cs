using Microsoft.AspNetCore.Builder;
using Prometheus;

namespace FluxoCaixa.Chassis.Metrics.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseMetrics(this WebApplication app)
        {
            app.UseRouting();
            app.UseHttpMetrics();
            app.MapMetrics();
        }
    }
}
