using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using FluxoCaixa.Services.CashIn.Applications.Dto.Order;
using FluxoCaixa.Services.CashIn.Applications.Interfaces.Services;
using System.Text.Json;

namespace FluxoCaixa.Services.CashIn.Applications.Services
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
                Service = AppServices.CashIn
            };
            await _producer.SendAsync(AppServices.Wallet, "TransactionConsumer", JsonSerializer.Serialize(data));
        }
    }
}
