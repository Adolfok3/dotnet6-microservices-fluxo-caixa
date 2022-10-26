using FluxoCaixa.Chassis.Authentication.Utils.Interfaces;
using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Interfaces.Services;
using FluxoCaixa.Services.Authentication.Application.Mappers;
using FluxoCaixa.Services.Authentication.Application.Services;
using FluxoCaixa.Services.Authentication.Domain.Entities;
using FluxoCaixa.Services.Authentication.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Security.Claims;
using System.Text;

namespace FluxoCaixa.Tests.Authentication.Applications.Services
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        private Mock<IUserRepository> _repo;
        private Mock<IJwtGenerator> _jwt;
        private Mock<IDistributedCache> _cache;
        private IAuthenticationService _service;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUserRepository>();
            _jwt = new Mock<IJwtGenerator>();
            _cache = new Mock<IDistributedCache>();
            _service = new AuthenticationService(_repo.Object, _jwt.Object, _cache.Object, TestHelper.GetMapper(typeof(EntityXDtoMapper)));
        }

        [TearDown]
        public void TearDown()
        {
            _repo.VerifyNoOtherCalls();
            _jwt.VerifyNoOtherCalls();
            _cache.VerifyNoOtherCalls();
        }

        [Test]
        public void LoginAsyncWithNonUserShouldBeInvalidCredentials()
        {
            var dto = new UserAuthenticationRequestDto
            {
                Password = "test",
                Username = "test"
            };
            var test = new AsyncTestDelegate(() => _service.LoginAsync(dto));
            Assert.ThrowsAsync<ArgumentException>(test, "Username or password invalid.");

            _repo.Verify(v => v.GetUserByUsernameAsync("test"), Times.Once);
        }

        [Test]
        public void LoginAsyncWithInvalidPasswordShouldBeInvalidCredentials()
        {
            _repo.Setup(s => s.GetUserByUsernameAsync("test")).ReturnsAsync(new User
            {
                Password = "test"
            });
            var dto = new UserAuthenticationRequestDto
            {
                Password = "test",
                Username = "test"
            };
            var test = new AsyncTestDelegate(() => _service.LoginAsync(dto));
            Assert.ThrowsAsync<ArgumentException>(test, "Username or password invalid.");

            _repo.Verify(v => v.GetUserByUsernameAsync("test"), Times.Once);
        }

        [Test]
        public async Task LoginAsyncShouldBeSuccessAsync()
        {
            var hash = new PasswordHasher<User>();
            var user = new User
            {
                BirthDate = DateTime.Now.AddYears(-18),
                CreatedAt = DateTime.Now,
                Email = "user@test.com",
                Phone = "+5511900000000",
                Role = "user",
                Username = "test",
                Fullname = "user test",
                Id = Guid.NewGuid()
            };
            user.Password = hash.HashPassword(user, "test");
            _repo.Setup(s => s.GetUserByUsernameAsync("test")).ReturnsAsync(user);

            _jwt.Setup(s => s.GenerateToken(user.Id, user.Fullname, user.Role)).Returns(("AccessToken", 1000));
            _jwt.Setup(s => s.GenerateRefreshToken()).Returns(("RefreshToken", 1000));

            var dto = new UserAuthenticationRequestDto
            {
                Password = "test",
                Username = "test"
            };
            var result = await _service.LoginAsync(dto);

            Assert.That(result.Email, Is.EqualTo("user@test.com"));
            Assert.That(result.Phone, Is.EqualTo("+5511900000000"));
            Assert.That(result.Role, Is.EqualTo("user"));
            Assert.That(result.Username, Is.EqualTo("test"));
            Assert.That(result.Fullname, Is.EqualTo("user test"));
            Assert.That(result.AccessToken, Is.EqualTo("AccessToken"));
            Assert.That(result.RefreshToken, Is.EqualTo("RefreshToken"));
            Assert.That(result.BirthDate, Is.Not.EqualTo(DateTime.MinValue));
            Assert.That(result.CreatedAt, Is.Not.EqualTo(DateTime.MinValue));
            Assert.That(result.AccessTokenExpirationDate, Is.Not.EqualTo(DateTime.MinValue));
            Assert.That(result.RefreshTokenExpirationDate, Is.Not.EqualTo(DateTime.MinValue));

            _repo.Verify(v => v.GetUserByUsernameAsync("test"), Times.Once);
            _jwt.Verify(v => v.GenerateToken(user.Id, user.Fullname, user.Role), Times.Once);
            _jwt.Verify(v => v.GenerateRefreshToken(), Times.Once);
            _cache.Verify(s => s.SetAsync($"user_refresh_token_cache_{user.Id}", It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), Times.Once());
        }

        [Test]
        public void RefreshTokenAsyncShouldBeInvalidRefreshToken()
        {
            var userId = Guid.NewGuid();
            _jwt.Setup(s => s.GetClaimsFromToken("accesstoken")).Returns(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString())
            });
            _cache.Setup(s => s.GetAsync($"user_refresh_token_cache_{userId}", default)).ReturnsAsync(Encoding.UTF8.GetBytes("otherrefreshtoken"));

            var test = new AsyncTestDelegate(() => _service.RefreshTokenAsync("accesstoken", "refreshtoken"));
            Assert.ThrowsAsync<ArgumentException>(test, "Invalid refresh token.");

            _jwt.Verify(v => v.GetClaimsFromToken("accesstoken"), Times.Once);
            _cache.Verify(v => v.GetAsync($"user_refresh_token_cache_{userId}", default), Times.Once);
        }

        [Test]
        public async Task RefreshTokenAsyncShouldBeSuccessAsync()
        {
            var userId = Guid.NewGuid();
            _jwt.Setup(s => s.GetClaimsFromToken("accesstoken")).Returns(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString())
            });
            _cache.Setup(s => s.GetAsync($"user_refresh_token_cache_{userId}", default)).ReturnsAsync(Encoding.UTF8.GetBytes("refreshtoken"));
            _jwt.Setup(s => s.GenerateToken(It.IsAny<IEnumerable<Claim>>())).Returns(("accesstoken", 1000));
            _jwt.Setup(s => s.GenerateRefreshToken()).Returns(("refreshtoken", 1000));

            var result = await _service.RefreshTokenAsync("accesstoken", "refreshtoken");
            Assert.That(result.AccessToken, Is.EqualTo("accesstoken"));
            Assert.That(result.RefreshToken, Is.EqualTo("refreshtoken"));
            Assert.That(result.AccessTokenExpirationDate, Is.Not.EqualTo(DateTime.MinValue));
            Assert.That(result.RefreshTokenExpirationDate, Is.Not.EqualTo(DateTime.MinValue));

            _jwt.Verify(v => v.GetClaimsFromToken("accesstoken"), Times.Once);
            _jwt.Verify(v => v.GenerateToken(It.IsAny<IEnumerable<Claim>>()), Times.Once);
            _jwt.Verify(v => v.GenerateRefreshToken(), Times.Once);
            _cache.Verify(v => v.GetAsync($"user_refresh_token_cache_{userId}", default), Times.Once);
            _cache.Verify(v => v.SetAsync($"user_refresh_token_cache_{userId}", It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), Times.Once);
        }
    }
}
