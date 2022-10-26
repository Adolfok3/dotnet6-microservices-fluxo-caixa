using FluxoCaixa.Chassis.Persistence.Common;

namespace FluxoCaixa.Services.Wallet.Domain.Entities
{
    public class Transaction : Entity
    {
        public long Number { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Service { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; }
    }
}
