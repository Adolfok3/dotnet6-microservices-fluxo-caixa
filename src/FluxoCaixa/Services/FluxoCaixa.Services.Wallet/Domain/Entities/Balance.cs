using FluxoCaixa.Chassis.Persistence.Common;

namespace FluxoCaixa.Services.Wallet.Domain.Entities
{
    public class Balance : Entity
    {
        public decimal Value { get; set; }
    }
}
