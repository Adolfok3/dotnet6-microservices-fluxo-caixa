using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using Moq;
using System.Net;

namespace FluxoCaixa.Tests.Wallet.Controllers
{
    [TestFixture]
    public class BalanceControllerTest
    {
        private HttpClient _client;
        private Mock<IBalanceService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IBalanceService>();
            _client = TestHelper.CreateClient<Program>(new TestService
            {
                Type = typeof(IBalanceService),
                Implementation = _service.Object
            });
        }

        [TearDown]
        public void TearDown()
        {
            _service.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetBalanceShouldBeSuccessAsync()
        {
            var token = TestHelper.GenerateFakeAccessToken();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            var response = await _client.GetAsync("/balance");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            _service.Verify(v => v.GetBalanceAsync(), Times.Once);
        }
    }
}
