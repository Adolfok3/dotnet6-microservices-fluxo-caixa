using FluxoCaixa.Services.CashOut.Applications.Dto.Order;

namespace FluxoCaixa.Services.CashOut.Applications.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderCreateDto dto, string accessToken);
    }
}
