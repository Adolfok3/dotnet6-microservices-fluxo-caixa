using FluentValidation;
using FluentValidation.Results;

namespace FluxoCaixa.Chassis.Utils.Extensions
{
    public static class FluentValidationExtensions
    {
        public static async Task ValidateDtoAsync<T>(this IValidator<T> validator, T model)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.GetErrorMessage());
        }

        private static string GetErrorMessage(this ValidationResult result)
        {
            var errorMessage = result.Errors.FirstOrDefault()?.ErrorMessage;
            return string.IsNullOrEmpty(errorMessage) ? "There was a problem with your request." : errorMessage;
        }
    }
}
