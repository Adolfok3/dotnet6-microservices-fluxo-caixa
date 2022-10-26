using FluxoCaixa.Chassis.Logging.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Logging.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAppLogger, AppLogger>();
            builder.Services.AddSingleton<IErrorLogger, ErrorLogger>();
            builder.Services.AddSingleton<IHttpLogger, HttpLogger>();
        }
    }
}
