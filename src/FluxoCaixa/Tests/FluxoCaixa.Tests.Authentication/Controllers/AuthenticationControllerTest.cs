using FluxoCaixa.Chassis.Utils.Common;
using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using Moq;
using System.Net;
using System.Net.Http.Json;
using IAuthenticationService = FluxoCaixa.Services.Authentication.Application.Interfaces.Services.IAuthenticationService;

namespace FluxoCaixa.Tests.Authentication.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTest
    {
        private HttpClient _client;
        private Mock<IAuthenticationService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IAuthenticationService>();
            _client = TestHelper.CreateClient<Program>(new TestService
            {
                Type = typeof(IAuthenticationService),
                Implementation = _service.Object
            });
        }

        [TearDown]
        public void TearDown()
        {
            _service.VerifyNoOtherCalls();
        }

        [Test]
        public async Task LoginAsyncShouldBeSuccessAsync()
        {
            _service.Setup(s => s.LoginAsync(It.IsAny<UserAuthenticationRequestDto>())).ReturnsAsync(new UserAuthenticationResponseDto());
            var response = await _client.PostAsJsonAsync("/authentication/login", new { });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            _service.Verify(v => v.LoginAsync(It.IsAny<UserAuthenticationRequestDto>()), Times.Once);
        }

        [Test]
        public async Task RefreshTokenAsyncShouldBeSuccessAsync()
        {
            var token = TestHelper.GenerateFakeAccessToken();
            _service.Setup(s => s.RefreshTokenAsync("accesstoken", "refreshtoken")).ReturnsAsync(new UserAuthenticationRefreshTokenResponseDto());
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            var response = await _client.PostAsJsonAsync("/authentication/refresh-token", new UserAuthenticationRefreshTokenRequestDto
            {
                RefreshToken = "refreshtoken"
            });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            _service.Verify(v => v.RefreshTokenAsync(token, "refreshtoken"), Times.Once);
        }
    }
}
