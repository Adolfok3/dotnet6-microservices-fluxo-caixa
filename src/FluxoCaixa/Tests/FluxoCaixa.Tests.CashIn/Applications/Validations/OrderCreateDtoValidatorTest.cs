using FluentValidation.TestHelper;
using FluxoCaixa.Services.CashIn.Applications.Dto.Order;
using FluxoCaixa.Services.CashIn.Applications.Validations.Order;

namespace FluxoCaixa.Tests.CashIn.Applications.Validations
{
    [TestFixture]
    public class OrderCreateDtoValidatorTest
    {
        private OrderCreateDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new OrderCreateDtoValidator();
        }

        [Test]
        public async Task ValidateAsyncShouldHaveErrorOnAllPropertiesAsync()
        {
            var dto = new OrderCreateDto();
            var result = await _validator.TestValidateAsync(dto);
            result.ShouldHaveValidationErrorFor(p => p.Description);
            result.ShouldHaveValidationErrorFor(p => p.Value);
            result.ShouldHaveValidationErrorFor(p => p.Number);
        }


        [Test]
        public async Task ValidateAsyncShouldNotHaveAnyErrorAsync()
        {
            var dto = new OrderCreateDto
            {
                Value = 10.99M,
                Description = "test",
                Number = 123
            };
            var result = await _validator.TestValidateAsync(dto);
            result.ShouldNotHaveValidationErrorFor(p => p.Description);
            result.ShouldNotHaveValidationErrorFor(p => p.Value);
            result.ShouldNotHaveValidationErrorFor(p => p.Number);
        }
    }
}
