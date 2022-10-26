using FluentValidation.TestHelper;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Validations.Authentication;

namespace FluxoCaixa.Tests.Authentication.Applications.Validations
{
    [TestFixture]
    public class UserAuthenticationRequestDtoValidatorTest
    {
        private UserAuthenticationRequestDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UserAuthenticationRequestDtoValidator();
        }

        [Test]
        public async Task ValidateAsyncShouldHaveErrorWithUsernameNullAsync()
        {
            var dto = new UserAuthenticationRequestDto();
            var result = await _validator.TestValidateAsync(dto);
            result.ShouldHaveValidationErrorFor(p => p.Username);
        }

        [Test]
        public async Task ValidateAsyncShouldHaveErrorWithPasswordNullAsync()
        {
            var dto = new UserAuthenticationRequestDto
            {
                Username = "test"
            };
            var result = await _validator.TestValidateAsync(dto);
            result.ShouldHaveValidationErrorFor(p => p.Password);
            result.ShouldNotHaveValidationErrorFor(p => p.Username);
        }

        [Test]
        public async Task ValidateAsyncShouldBeSuccessAsync()
        {
            var dto = new UserAuthenticationRequestDto
            {
                Username = "test",
                Password = "test"
            };
            var result = await _validator.TestValidateAsync(dto);
            result.ShouldNotHaveValidationErrorFor(p => p.Username);
            result.ShouldNotHaveValidationErrorFor(p => p.Password);
        }
    }
}
