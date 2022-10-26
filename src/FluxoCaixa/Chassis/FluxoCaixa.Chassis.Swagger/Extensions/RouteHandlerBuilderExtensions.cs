using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoCaixa.Chassis.Swagger.Extensions
{
    public static class RouteHandlerBuilderExtensions
    {
        public static RouteHandlerBuilder WithDescription(this RouteHandlerBuilder route, string description)
        {
            route.WithMetadata(new SwaggerOperationAttribute(description, description));

            return route;
        }
    }
}
