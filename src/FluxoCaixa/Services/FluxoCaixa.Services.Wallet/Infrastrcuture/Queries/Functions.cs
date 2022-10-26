namespace FluxoCaixa.Services.Wallet.Infrastrcuture.Queries
{
    public static class Functions
    {
        public const string UpdateBalance = @"CREATE OR REPLACE FUNCTION Update_Balance() RETURNS trigger AS $Update_Balance$
              declare
              begin
              insert into public.""Balances""(""Id"", ""CreatedAt"", ""Value"")
                values('df203fff-79f1-4f6b-97de-1e8b4260babe', now(), new.""Value"")
                on conflict (""Id"") do update set ""Value"" = ""Balances"".""Value"" + new.""Value"";
              RETURN NEW;
              END;
              $Update_Balance$ LANGUAGE plpgsql;";
    }
}
