using FluxoCaixa.Chassis.Authentication.Extensions;
using FluxoCaixa.Chassis.AutoMapper.Extensions;
using FluxoCaixa.Chassis.Caching.Extensions;
using FluxoCaixa.Chassis.ExceptionHandler.Extensions;
using FluxoCaixa.Chassis.Logging.Extensions;
using FluxoCaixa.Chassis.Messaging.Extensions;
using FluxoCaixa.Chassis.Persistence.Extensions;
using FluxoCaixa.Chassis.Swagger.Extensions;
using FluxoCaixa.Chassis.Utils.Extensions;
using FluxoCaixa.Chassis.Vault.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using FluxoCaixa.Chassis.HealthCheck.Extensions;
using MvcJsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace FluxoCaixa.Chassis.Startup.Extentions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddDefaultSetup<T>(this WebApplicationBuilder builder) where T : DbContext
        {
            if (builder.Environment.IsEnvironment("Testing"))
            {
                builder.AddAuthentication();
                return;
            }

            builder.AddVault();
            builder.AddUtils();
            builder.AddSwagger();
            builder.AddCaching();
            builder.AddLogging();
            builder.AddExceptionHandler();
            builder.AddAuthentication();
            builder.AddMessaging();
            builder.AddAutoMapper();
            builder.AddHealthChecks();
            builder.AddAppDbContext<T>();

            builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.Configure<MvcJsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }
    }
}
