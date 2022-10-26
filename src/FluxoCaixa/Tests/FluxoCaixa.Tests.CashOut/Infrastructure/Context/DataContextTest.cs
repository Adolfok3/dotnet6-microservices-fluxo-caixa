using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.CashOut.Infrastrcuture.Context;

namespace FluxoCaixa.Tests.CashOut.Infrastructure.Context
{
    [TestFixture]
    public class DataContextTest
    {
        [Test]
        public void CreateDbContextWithoutOptionsShouldBeSuccess()
        {
            var context = new DataContext();
            Assert.IsNotNull(context);
        }

        [Test]
        public void CreateDbContextWithOptionsShouldBeSuccess()
        {
            var context = new DataContext(TestHelper.GetDbContextOptions<DataContext>());
            Assert.IsNotNull(context);
        }
    }
}
