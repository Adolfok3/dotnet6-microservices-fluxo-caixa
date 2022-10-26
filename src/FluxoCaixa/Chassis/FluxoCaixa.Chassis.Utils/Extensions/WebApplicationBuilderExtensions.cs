using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Utils.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddUtils(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAssemblyHelper, AssemblyHelper>();
            builder.Services.AddSingleton<IAppUserHelper, AppUserHelper>();
        }
    }
}
