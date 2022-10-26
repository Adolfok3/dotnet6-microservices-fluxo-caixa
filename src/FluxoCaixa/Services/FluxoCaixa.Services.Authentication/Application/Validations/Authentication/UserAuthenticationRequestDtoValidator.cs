using FluentValidation;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;

namespace FluxoCaixa.Services.Authentication.Application.Validations.Authentication
{
    public class UserAuthenticationRequestDtoValidator : AbstractValidator<UserAuthenticationRequestDto>
    {
        public UserAuthenticationRequestDtoValidator()
        {
            RuleFor(r => r.Username).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(r => r.Password).NotNull().NotEmpty().MaximumLength(200);
        }
    }
}
