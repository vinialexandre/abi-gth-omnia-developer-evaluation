using FluentValidation.Results;

namespace Abi.DeveloperEvaluation.Common.Validation;

public class ValidationErrorDetail
{
    public string Error { get; init; } = string.Empty;
    public string Detail { get; init; } = string.Empty;

    public static explicit operator ValidationErrorDetail(ValidationFailure failure)
    {
        return new ValidationErrorDetail
        {
            Error = failure.PropertyName,     
            Detail = failure.ErrorMessage
        };
    }
}
