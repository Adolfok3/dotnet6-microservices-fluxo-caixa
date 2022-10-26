namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Queries
{
    public static class Triggers
    {
        public const string UpdateBalance = @"
               DROP TRIGGER IF EXISTS Update_Balance on public.""Transactions"";
               CREATE TRIGGER Update_Balance AFTER INSERT ON public.""Transactions""
               FOR EACH ROW EXECUTE PROCEDURE Update_Balance();";
    }
}
