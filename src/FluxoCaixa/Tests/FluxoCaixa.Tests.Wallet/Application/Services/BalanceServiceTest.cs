using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using FluxoCaixa.Services.Wallet.Application.Services;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using Moq;

namespace FluxoCaixa.Tests.Wallet.Application.Services
{
    [TestFixture]
    public class BalanceServiceTest
    {
        private Mock<IBalanceRepository> _repo;
        private IBalanceService _service;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IBalanceRepository>();
            _service = new BalanceService(_repo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _repo.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetBalanceAsyncShouldBeEmptyValueAsync()
        {
            var balance = await _service.GetBalanceAsync();
            Assert.That(balance, Is.EqualTo(0));
            _repo.Verify(v => v.GetBalanceAsync(), Times.Once);
        }

        [Test]
        public async Task GetBalanceAsyncShouldBeNotEmptyValueAsync()
        {
            _repo.Setup(s => s.GetBalanceAsync()).ReturnsAsync(new Balance
            {
                Value = 100
            });
            var balance = await _service.GetBalanceAsync();
            Assert.That(balance, Is.EqualTo(100));
            _repo.Verify(v => v.GetBalanceAsync(), Times.Once);
        }
    }
}
