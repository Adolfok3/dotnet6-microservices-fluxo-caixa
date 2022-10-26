using FluxoCaixa.Services.CashIn.Applications.Dto.Order;

namespace FluxoCaixa.Services.CashIn.Applications.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderCreateDto dto, string accessToken);
    }
}
