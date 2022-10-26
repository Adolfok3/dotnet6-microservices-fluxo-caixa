using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Domain.Entities;

namespace FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync(TransactionFilterDto dto);
        Task AddAsync(Transaction transaction);
    }
}
