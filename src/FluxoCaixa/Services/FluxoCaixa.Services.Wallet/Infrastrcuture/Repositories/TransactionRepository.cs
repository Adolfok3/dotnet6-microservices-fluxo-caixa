using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Context;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(TransactionFilterDto dto)
        {
            var query = _context.Transactions.AsQueryable();
            if (!string.IsNullOrEmpty(dto.Search))
            {
                var search = dto.Search.ToLower();
                query = query.Where(w => w.Description.ToLower().Contains(search) ||
                                         w.Service.ToLower().Contains(search) ||
                                         w.SellerName.ToLower().Contains(search));

            }

            if (dto.MaxCreatedAt.HasValue)
                query = query.Where(w => w.CreatedAt <= dto.MaxCreatedAt);

            if (dto.MinCreatedAt.HasValue)
                query = query.Where(w => w.CreatedAt >= dto.MinCreatedAt);

            return await query.OrderByDescending(o => o.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
