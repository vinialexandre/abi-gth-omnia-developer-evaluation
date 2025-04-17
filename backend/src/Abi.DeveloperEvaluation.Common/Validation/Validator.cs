using FluentValidation;

namespace Abi.DeveloperEvaluation.Common.Validation;

public static class Validator
{
    public static async Task<IEnumerable<ValidationErrorDetail>> ValidateAsync<T>(T instance, IValidator<T> validator)
    {
        var result = await validator.ValidateAsync(instance);

        return result.IsValid
            ? []
            : result.Errors.Select(e => (ValidationErrorDetail)e);
    }
}
