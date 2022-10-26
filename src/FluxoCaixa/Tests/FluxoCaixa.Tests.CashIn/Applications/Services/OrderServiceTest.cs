using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers.Interfaces;
using FluxoCaixa.Services.CashIn.Applications.Dto.Order;
using FluxoCaixa.Services.CashIn.Applications.Interfaces.Services;
using FluxoCaixa.Services.CashIn.Applications.Services;
using Moq;

namespace FluxoCaixa.Tests.CashIn.Applications.Services
{
    [TestFixture]
    public class OrderServiceTest
    {
        private IOrderService _service;
        private Mock<IAppUserHelper> _helper;
        private Mock<IProducer> _producer;

        [SetUp]
        public void SetUp()
        {
            _helper = new Mock<IAppUserHelper>();
            _producer = new Mock<IProducer>();
            _service = new OrderService(_helper.Object, _producer.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _helper.VerifyNoOtherCalls();
            _producer.VerifyNoOtherCalls();
        }

        [Test]
        public void CreateOrderAsyncShouldBeSuccess()
        {
            _helper.Setup(s => s.GetUserFromAccessTokenasync("accessToken")).ReturnsAsync(new AppUser());
            var dto = new OrderCreateDto();
            Assert.DoesNotThrowAsync(() => _service.CreateAsync(dto, "accessToken"));
            _helper.Verify(v => v.GetUserFromAccessTokenasync("accessToken"), Times.Once);
            _producer.Verify(v => v.SendAsync("Wallet", "TransactionConsumer", It.IsAny<string>()), Times.Once);
        }
    }
}
