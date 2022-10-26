using FluentValidation;
using FluxoCaixa.Services.CashOut.Applications.Dto.Order;

namespace FluxoCaixa.Services.CashOut.Applications.Validations.Order
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(r => r.Description).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(r => r.Number).GreaterThan(0).LessThan(999999999);
            RuleFor(r => r.Value).LessThan(0).GreaterThan(-999999999);
        }
    }
}
