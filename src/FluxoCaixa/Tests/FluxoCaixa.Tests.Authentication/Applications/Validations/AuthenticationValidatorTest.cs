using FluentValidation;
using FluentValidation.Results;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Interfaces.Services;
using FluxoCaixa.Services.Authentication.Application.Validations.Authentication;
using Moq;

namespace FluxoCaixa.Tests.Authentication.Applications.Validations
{
    [TestFixture]
    public class AuthenticationValidatorTest
    {
        private IAuthenticationService _validator;
        private Mock<IValidator<UserAuthenticationRequestDto>> _dtoValidator;
        private Mock<IAuthenticationService> _inner;

        [SetUp]
        public void SetUp()
        {
            _dtoValidator = new Mock<IValidator<UserAuthenticationRequestDto>>();
            _inner = new Mock<IAuthenticationService>();
            _validator = new AuthenticationValidator<IAuthenticationService>(_inner.Object, _dtoValidator.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _inner.VerifyNoOtherCalls();
            _dtoValidator.VerifyNoOtherCalls();
        }

        [Test]
        public void LoginShouldBeInvalidDto()
        {
            _dtoValidator.Setup(v => v.ValidateAsync(It.IsAny<UserAuthenticationRequestDto>(), default)).ReturnsAsync(new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new ("test", "test")
                }
            });
            var test = new AsyncTestDelegate(() => _validator.LoginAsync(new UserAuthenticationRequestDto()));
            Assert.ThrowsAsync<ArgumentException>(test, "test");
            _dtoValidator.Verify(v => v.ValidateAsync(It.IsAny<UserAuthenticationRequestDto>(), default), Times.Once);
        }

        [Test]
        public void LoginShouldBeSuccess()
        {
            _dtoValidator.Setup(v => v.ValidateAsync(It.IsAny<UserAuthenticationRequestDto>(), default)).ReturnsAsync(new ValidationResult());
            var test = new AsyncTestDelegate(() => _validator.LoginAsync(new UserAuthenticationRequestDto()));
            Assert.DoesNotThrowAsync(test);
            _dtoValidator.Verify(v => v.ValidateAsync(It.IsAny<UserAuthenticationRequestDto>(), default), Times.Once);
            _inner.Verify(v => v.LoginAsync(It.IsAny<UserAuthenticationRequestDto>()), Times.Once);
        }

        [Test]
        public void RefreshTokenShouldBeSuccess()
        {
            var test = new AsyncTestDelegate(() => _validator.RefreshTokenAsync("accesstoken", "refreshtoken"));
            Assert.DoesNotThrowAsync(test);
            _inner.Verify(v => v.RefreshTokenAsync("accesstoken", "refreshtoken"), Times.Once);
        }
    }
}
