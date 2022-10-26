namespace FluxoCaixa.Services.Wallet.Application.Interfaces.Services
{
    public interface IBalanceService
    {
        Task<decimal> GetBalanceAsync();
    }
}
