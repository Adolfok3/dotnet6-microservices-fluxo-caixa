using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Balance> Balances { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BalanceEntityTypeConfiguration());
        }
    }
}
