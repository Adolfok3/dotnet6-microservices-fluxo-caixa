using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using FluxoCaixa.Services.Wallet.Application.Mappers;
using FluxoCaixa.Services.Wallet.Application.Services;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using Moq;

namespace FluxoCaixa.Tests.Wallet.Application.Services
{
    [TestFixture]
    public class TransactionServiceTest
    {
        private ITransactionService _service;
        private Mock<ITransactionRepository> _repo;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ITransactionRepository>();
            _service = new TransactionService(_repo.Object, TestHelper.GetMapper(typeof(EntityXDtoMapper)));
        }

        [TearDown]
        public void TearDown()
        {
            _repo.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAllAsyncShouldBeSuccessAsync()
        {
            _repo.Setup(s => s.GetAllAsync(It.IsAny<TransactionFilterDto>())).ReturnsAsync(new List<Transaction>
            {
                new()
                {
                    Value = 100,
                    Description = "test",
                    Number = 1,
                    Service = "CashIn",
                    SellerName = "test",
                    SellerId = Guid.NewGuid()
                }
            });
            var dto = new TransactionFilterDto();
            var list = await _service.GetAllAsync(dto);

            Assert.That(list, Is.Not.Null);
            Assert.That(list, Is.Not.Empty);

            var item = list.First();
            Assert.That(item.SellerName, Is.EqualTo("test"));
            Assert.That(item.Description, Is.EqualTo("test"));
            Assert.That(item.Service, Is.EqualTo("CashIn"));
            Assert.That(item.Value, Is.EqualTo(100));
            Assert.That(item.Number, Is.EqualTo(1));
            Assert.That(item.SellerId, Is.Not.EqualTo(Guid.Empty));

            _repo.Verify(v => v.GetAllAsync(It.IsAny<TransactionFilterDto>()), Times.Once);
        }
    }
}
