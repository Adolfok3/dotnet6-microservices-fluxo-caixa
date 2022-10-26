using FluxoCaixa.Chassis.Swagger.Extensions;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;

namespace FluxoCaixa.Services.Wallet.Controllers
{
    public class BalanceController
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("balance", async (IBalanceService service) =>
                {
                    var balance = await service.GetBalanceAsync();
                    return AppResults.Ok(balance);
                })
                .Produces(200, typeof(DefaultResponse<decimal>))
                .WithDisplayName("Balance")
                .WithDescription("Get current balance")
                .RequireAuthorization("user");
        }
    }
}
