using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FluxoCaixa.Chassis.AutoMapper.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddAutoMapper(this WebApplicationBuilder builder)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddMaps(Assembly.GetEntryAssembly());
            });

            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);
        }
    }
}
