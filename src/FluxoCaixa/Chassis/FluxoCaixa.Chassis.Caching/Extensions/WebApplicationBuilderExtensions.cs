using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Caching.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddCaching(this WebApplicationBuilder builder)
        {
            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = builder.Configuration.GetValue<string>("RedisConnectionString");
                opt.InstanceName = "FluxoCaixa_";
            });
        }
    }
}
