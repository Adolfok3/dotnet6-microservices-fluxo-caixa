using FluxoCaixa.Services.Authentication.Domain.Entities;
using FluxoCaixa.Services.Authentication.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Services.Authentication.Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
