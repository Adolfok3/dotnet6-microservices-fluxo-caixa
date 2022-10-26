using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Context;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly DataContext _context;

        public BalanceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Balance> GetBalanceAsync()
        {
            return await _context.Balances.SingleOrDefaultAsync();
        }
    }
}
