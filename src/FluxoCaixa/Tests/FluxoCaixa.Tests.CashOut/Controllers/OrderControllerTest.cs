using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.CashOut.Applications.Dto.Order;
using FluxoCaixa.Services.CashOut.Applications.Interfaces.Services;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace FluxoCaixa.Tests.CashOut.Controllers
{
    [TestFixture]
    public class OrderControllerTest
    {
        private HttpClient _client;
        private Mock<IOrderService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IOrderService>();
            _client = TestHelper.CreateClient<Program>(new TestService
            {
                Type = typeof(IOrderService),
                Implementation = _service.Object
            });
        }

        [TearDown]
        public void TearDown()
        {
            _service.VerifyNoOtherCalls();
        }

        [Test]
        public async Task CreateOrderAsyncShouldBeSuccessAsync()
        {
            var data = new OrderCreateDto
            {
                Value = 100,
                Description = "test",
                Number = 1
            };
            var token = TestHelper.GenerateFakeAccessToken();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            var response = await _client.PostAsJsonAsync("/orders", data);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            _service.Verify(v => v.CreateAsync(It.IsAny<OrderCreateDto>(), token), Times.Once);
        }
    }
}
