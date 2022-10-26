using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Wallet.Application.Dto.Transaction;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using Moq;
using System.Net;

namespace FluxoCaixa.Tests.Wallet.Controllers
{
    [TestFixture]
    public class TransactionControllerTest
    {
        private HttpClient _client;
        private Mock<ITransactionService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ITransactionService>();
            _client = TestHelper.CreateClient<Program>(new TestService
            {
                Type = typeof(ITransactionService),
                Implementation = _service.Object
            });
        }

        [TearDown]
        public void TearDown()
        {
            _service.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetAllTransactionsShouldBeSuccessAsync()
        {
            var token = TestHelper.GenerateFakeAccessToken();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            var response = await _client.GetAsync("/transactions");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            _service.Verify(v => v.GetAllAsync(It.IsAny<TransactionFilterDto>()), Times.Once);
        }

        [Test]
        public async Task GetDailyTransactionsShouldBeSuccessAsync()
        {
            var token = TestHelper.GenerateFakeAccessToken();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            var response = await _client.GetAsync("/transactions/daily");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            _service.Verify(v => v.GetAllAsync(It.IsAny<TransactionFilterDto>()), Times.Once);
        }
    }
}
