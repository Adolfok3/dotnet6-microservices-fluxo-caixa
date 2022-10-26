namespace FluxoCaixa.Services.Wallet.Application.Dto.Transaction
{
    public class TransactionFilterDto
    {
        public string Search { get; set; }
        public DateTimeOffset? MinCreatedAt { get; set; }
        public DateTimeOffset? MaxCreatedAt { get; set; }
    }
}
