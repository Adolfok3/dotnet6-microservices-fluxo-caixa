using FluxoCaixa.Services.Wallet.Domain.Entities;

namespace FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> GetBalanceAsync();
    }
}
