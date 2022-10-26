using System.Text.Json;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using FluxoCaixa.Services.CashOut.Applications.Dto.Order;
using FluxoCaixa.Services.CashOut.Applications.Interfaces.Services;

namespace FluxoCaixa.Services.CashOut.Applications.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAppUserHelper _appUserHelper;
        private readonly IProducer _producer;

        public OrderService(IAppUserHelper appUserHelper, IProducer producer)
        {
            _appUserHelper = appUserHelper;
            _producer = producer;
        }

        public async Task CreateAsync(OrderCreateDto dto, string accessToken)
        {
            var user = await _appUserHelper.GetUserFromAccessTokenasync(accessToken);
            var data = new AppOrderDto
            {
                SellerId = user.UserId,
                SellerName = user.FullName,
                Value = dto.Value,
                Description = dto.Description,
                Number = dto.Number,
                Service = AppServices.CashOut
            };
            await _producer.SendAsync(AppServices.Wallet, "TransactionConsumer", JsonSerializer.Serialize(data));
        }
    }
}
