using FluentValidation;
using FluentValidation.Results;
using FluxoCaixa.Services.CashIn.Applications.Dto.Order;
using FluxoCaixa.Services.CashIn.Applications.Interfaces.Services;
using FluxoCaixa.Services.CashIn.Applications.Validations.Order;
using Moq;

namespace FluxoCaixa.Tests.CashIn.Applications.Validations
{
    [TestFixture]
    public class OrderValidatorTest
    {
        private OrderValidator<IOrderService> _validator;
        private Mock<IValidator<OrderCreateDto>> _dtoValidator;
        private Mock<IOrderService> _inner;

        [SetUp]
        public void SetUp()
        {
            _inner = new Mock<IOrderService>();
            _dtoValidator = new Mock<IValidator<OrderCreateDto>>();
            _validator = new OrderValidator<IOrderService>(_inner.Object, _dtoValidator.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dtoValidator.VerifyNoOtherCalls();
            _inner.VerifyNoOtherCalls();
        }

        [Test]
        public void CreateAsyncShouldBeSuccess()
        {
            _dtoValidator.Setup(v => v.ValidateAsync(It.IsAny<OrderCreateDto>(), default)).ReturnsAsync(new ValidationResult
            {
                Errors = new List<ValidationFailure>()
            });
            var dto = new OrderCreateDto
            {
                Value = 100,
                Description = "test",
                Number = 1234
            };
            Assert.DoesNotThrowAsync(() => _validator.CreateAsync(dto, "accessToken"));
            _inner.Verify(v => v.CreateAsync(It.IsAny<OrderCreateDto>(), "accessToken"));
            _dtoValidator.Verify(v => v.ValidateAsync(It.IsAny<OrderCreateDto>(), default), Times.Once);
        }

        [Test]
        public void CreateAsyncShouldThrowsArgumentException()
        {
            _dtoValidator.Setup(v => v.ValidateAsync(It.IsAny<OrderCreateDto>(), default)).ReturnsAsync(new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new ("test", "test")
                }
            });
            var dto = new OrderCreateDto();
            Assert.ThrowsAsync<ArgumentException>(() => _validator.CreateAsync(dto, "accessToken"), "test");
            _dtoValidator.Verify(v => v.ValidateAsync(It.IsAny<OrderCreateDto>(), default), Times.Once);
        }
    }
}
