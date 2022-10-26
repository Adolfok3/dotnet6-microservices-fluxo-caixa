using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.CashIn.Infrastrcuture.Context;

namespace FluxoCaixa.Tests.CashIn.Infrastructure.Context
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
