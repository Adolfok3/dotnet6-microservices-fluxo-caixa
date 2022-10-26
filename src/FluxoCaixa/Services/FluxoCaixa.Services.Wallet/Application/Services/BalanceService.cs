using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;

namespace FluxoCaixa.Services.Wallet.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _repository;

        public BalanceService(IBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> GetBalanceAsync()
        {
            var balance = await _repository.GetBalanceAsync();
            return balance?.Value ?? 0.00M;
        }
    }
}
