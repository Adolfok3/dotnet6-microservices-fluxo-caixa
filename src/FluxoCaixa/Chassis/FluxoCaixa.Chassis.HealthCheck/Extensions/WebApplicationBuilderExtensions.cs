using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace FluxoCaixa.Chassis.HealthCheck.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddHealthChecks(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks().ForwardToPrometheus();
        }
    }
}
