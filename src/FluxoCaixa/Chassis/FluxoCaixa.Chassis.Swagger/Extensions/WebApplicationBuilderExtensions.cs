using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Chassis.Swagger.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.EnableAnnotations();
                opt.TagActionsBy(d => new List<string> { d.ActionDescriptor.DisplayName });
            });
        }
    }
}
