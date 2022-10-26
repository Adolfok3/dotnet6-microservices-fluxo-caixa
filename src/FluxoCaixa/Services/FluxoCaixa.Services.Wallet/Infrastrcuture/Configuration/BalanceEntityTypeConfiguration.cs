using FluxoCaixa.Chassis.Persistence.Configuration;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Configuration
{
    public class BalanceEntityTypeConfiguration : EntityTypeConfiguration<Balance>
    {
        public override void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.Property(p => p.Value).HasPrecision(16, 2);
            base.Configure(builder);
        }
    }
}
