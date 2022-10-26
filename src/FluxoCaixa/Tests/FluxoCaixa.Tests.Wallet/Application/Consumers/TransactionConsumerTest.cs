using FluxoCaixa.Chassis.Logging.Interfaces;
using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Services.Wallet.Application.Consumers;
using FluxoCaixa.Services.Wallet.Domain.Entities;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using Moq;
using System.Text.Json;

namespace FluxoCaixa.Tests.Wallet.Application.Consumers
{
    [TestFixture]
    public class TransactionConsumerTest
    {
        private IConsumer _consumer;
        private Mock<ITransactionRepository> _repo;
        private Mock<IErrorLogger> _logger;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ITransactionRepository>();
            _logger = new Mock<IErrorLogger>();

            _consumer = new TransactionConsumer(_repo.Object, _logger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _repo.VerifyNoOtherCalls();
            _logger.VerifyNoOtherCalls();
        }

        [Test]
        public void OnExecutingShouldBeSuccess()
        {
            var data = JsonSerializer.Serialize(new AppOrderDto());
            Assert.DoesNotThrowAsync(() => _consumer.OnExecuting(data));
            _repo.Verify(v => v.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Test]
        public void OnExecutingShouldLogError()
        {
            Assert.DoesNotThrowAsync(() => _consumer.OnExecuting("test"));
            _repo.Verify(v => v.AddAsync(It.IsAny<Transaction>()), Times.Never);
            _logger.Verify(v => v.Fatal(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetKeyShouldBeEqualToNameClass()
        {
            var key = _consumer.GetKey();
            Assert.That(key, Is.EqualTo("TransactionConsumer"));
        }
    }
}
