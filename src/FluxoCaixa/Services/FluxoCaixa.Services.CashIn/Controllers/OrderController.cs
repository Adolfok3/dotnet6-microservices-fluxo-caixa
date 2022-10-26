using FluxoCaixa.Chassis.ExceptionHandler.Models;
using FluxoCaixa.Chassis.Swagger.Extensions;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Extensions;
using FluxoCaixa.Services.CashIn.Applications.Dto.Order;
using FluxoCaixa.Services.CashIn.Applications.Interfaces.Services;

namespace FluxoCaixa.Services.CashIn.Controllers
{
    public class OrderController
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapPost("orders", async (HttpRequest request, IOrderService service, OrderCreateDto dto) =>
                {
                    await service.CreateAsync(dto, request.GetAuthorizationHeader());
                    return AppResults.NoContent();
                })
                .Produces(204)
                .Produces(400, typeof(ErrorResponse))
                .WithDisplayName("Orders")
                .WithDescription("Create a new cashin order")
                .RequireAuthorization("user");
        }
    }
}
