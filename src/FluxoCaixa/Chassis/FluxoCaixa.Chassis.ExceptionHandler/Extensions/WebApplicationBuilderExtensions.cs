using FluxoCaixa.Chassis.ExceptionHandler.Dictionaries;
using FluxoCaixa.Chassis.ExceptionHandler.Dictionaries.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.ExceptionHandler.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddExceptionHandler(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IExceptionHandlerStatusCodeDictionary, ExceptionHandlerStatusCodeDictionary>();
        }
    }
}
