using FluxoCaixa.Chassis.Swagger.Extensions;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Services.Wallet.Controllers
{
    public class TransactionController
    {
        private const string Endpoint = "transactions";

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet(Endpoint, async (ITransactionService service, [FromQuery]string search, [FromQuery] DateTimeOffset? maxCreatedAt, [FromQuery] DateTimeOffset? minCreatedAt) =>
                {
                    var dto = new TransactionFilterDto
                    {
                        MaxCreatedAt = maxCreatedAt,
                        MinCreatedAt = minCreatedAt,
                        Search = search
                    };
                    var transactions = await service.GetAllAsync(dto);
                    return AppResults.Ok(transactions);
                })
                .Produces(200, typeof(DefaultResponse<IEnumerable<TransactionDto>>))
                .WithDisplayName("Transactions")
                .WithDescription("Get all transactions by filter")
                .RequireAuthorization("user");

            app.MapGet($"{Endpoint}/daily", async (ITransactionService service) =>
                {
                    var now = DateTime.UtcNow;
                    var dto = new TransactionFilterDto
                    {
                        MaxCreatedAt = now,
                        MinCreatedAt = new DateTimeOffset(now.Year, now.Month, now.Day, 0, 0, 0, TimeSpan.Zero)
                    };
                    var transactions = await service.GetAllAsync(dto);
                    return AppResults.Ok(transactions);
                })
                .Produces(200, typeof(DefaultResponse<IEnumerable<TransactionDto>>))
                .WithDisplayName("Transactions")
                .WithDescription("Get all transactions created today")
                .RequireAuthorization("user");
        }
    }
}
