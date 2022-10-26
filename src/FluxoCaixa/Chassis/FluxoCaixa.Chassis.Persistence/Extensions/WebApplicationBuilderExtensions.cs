using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Persistence.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddAppDbContext<T>(this WebApplicationBuilder builder) where T : DbContext
        {
            var connectionString = builder.Configuration.GetValue<string>("ConnectionString");
            builder.Services.AddDbContext<T>(opt =>
            {
                if (string.IsNullOrEmpty(connectionString))
                    opt.UseInMemoryDatabase("FluxoCaixa");
                else
                    opt.UseNpgsql(connectionString);
            }, ServiceLifetime.Singleton);
        }
    }
}
