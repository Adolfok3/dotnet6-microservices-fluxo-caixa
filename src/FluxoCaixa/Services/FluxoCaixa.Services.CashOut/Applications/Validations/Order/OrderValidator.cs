using FluentValidation;
using FluxoCaixa.Chassis.Utils.Extensions;
using FluxoCaixa.Services.CashOut.Applications.Dto.Order;
using FluxoCaixa.Services.CashOut.Applications.Interfaces.Services;

namespace FluxoCaixa.Services.CashOut.Applications.Validations.Order
{
    public class OrderValidator<T> : IOrderService where T : IOrderService
    {
        private readonly T _inner;
        private readonly IValidator<OrderCreateDto> _validator;

        public OrderValidator(T inner, IValidator<OrderCreateDto> validator)
        {
            _inner = inner;
            _validator = validator;
        }

        public async Task CreateAsync(OrderCreateDto dto, string accessToken)
        {
            await _validator.ValidateDtoAsync(dto);
            await _inner.CreateAsync(dto, accessToken);
        }
    }
}
