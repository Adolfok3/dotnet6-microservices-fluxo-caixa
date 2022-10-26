using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Context;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Repositories;

namespace FluxoCaixa.Tests.Wallet.Infrastructure.Repositories
{
    [TestFixture]
    public class BalanceRepositoryTest
    {
        private IBalanceRepository _repo;

        [SetUp]
        public void SetUp()
        {
            var context = new DataContext(TestHelper.GetDbContextOptions<DataContext>());
            _repo = new BalanceRepository(context);
        }

        [Test]
        public void GetBalanceShouldBeSuccess()
        {
            Assert.DoesNotThrowAsync(() => _repo.GetBalanceAsync());
        }
    }
}
