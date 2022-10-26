using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Context;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Repositories;

namespace FluxoCaixa.Tests.Wallet.Infrastructure.Repositories
{
    [TestFixture]
    public class TransactionRepositoryTest
    {
        private ITransactionRepository _repo;

        [SetUp]
        public void SetUp()
        {
            var context = new DataContext(TestHelper.GetDbContextOptions<DataContext>());
            _repo = new TransactionRepository(context);
        }

        [Test]
        public void GetAllShouldBeSuccess()
        {
            var dto = new TransactionFilterDto
            {
                Search = "test",
                MaxCreatedAt = DateTimeOffset.UtcNow,
                MinCreatedAt = DateTimeOffset.UtcNow
            };
            Assert.DoesNotThrowAsync(() => _repo.GetAllAsync(dto));
        }

        [Test]
        public void AddShouldBeSuccess()
        {
            var transaction = new Transaction
            {
                Value = 100,
                Description = "test",
                Number = 1,
                SellerId = Guid.NewGuid(),
                SellerName = "test",
                Service = "CashOut"
            };
            Assert.DoesNotThrowAsync(() => _repo.AddAsync(transaction));
        }
    }
}
