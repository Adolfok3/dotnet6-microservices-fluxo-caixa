using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;

namespace FluxoCaixa.Services.Wallet.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync(TransactionFilterDto dto);
    }
}
