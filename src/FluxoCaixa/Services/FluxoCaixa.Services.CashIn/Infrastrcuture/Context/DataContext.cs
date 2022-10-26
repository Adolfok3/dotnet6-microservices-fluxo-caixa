using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Services.CashIn.Infrastrcuture.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
