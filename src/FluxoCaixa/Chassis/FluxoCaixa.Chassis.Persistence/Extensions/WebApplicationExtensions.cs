using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Persistence.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void MigrateDatabase<T>(this WebApplication app) where T : DbContext
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<T>();

            if (context.Database.IsInMemory())
                return;

            context.Database.Migrate();
        }

        public static void ExecuteDatabaseQueries<T>(this WebApplication app, string environment, params string[] queries) where T : DbContext
        {
            if (environment == "Testing")
                return;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<T>();

            if (context.Database.IsInMemory())
                return;

            foreach (var query in queries)
            {
                context.Database.ExecuteSqlRaw(query);
            }
        }
    }
}
