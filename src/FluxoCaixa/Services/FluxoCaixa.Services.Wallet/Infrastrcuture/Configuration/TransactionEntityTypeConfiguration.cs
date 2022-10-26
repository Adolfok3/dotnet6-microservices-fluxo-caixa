using FluxoCaixa.Chassis.Persistence.Configuration;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Configuration
{
    public class TransactionEntityTypeConfiguration : EntityTypeConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Service).IsRequired().HasMaxLength(50);
            builder.Property(p => p.SellerName).IsRequired().HasMaxLength(70);
            builder.Property(p => p.SellerId).IsRequired().HasMaxLength(36);
            builder.Property(p => p.Value).HasPrecision(16, 2);

            builder.HasIndex(h => h.Number).IsUnique();

            base.Configure(builder);
        }
    }
}
