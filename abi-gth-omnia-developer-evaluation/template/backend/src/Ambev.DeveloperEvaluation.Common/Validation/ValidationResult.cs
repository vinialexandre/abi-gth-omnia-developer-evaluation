using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Common.Validation;

public class ValidationResultDetail
{
    public bool IsValid { get; set; }
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];

    public ValidationResultDetail()
    {
        
    }

    public ValidationResultDetail(ValidationResult validationResult)
    {
        IsValid = validationResult.IsValid;
        Errors = validationResult.Errors.Select(o => (ValidationErrorDetail)o);
    }
}
