using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetUserValidator : AbstractValidator<GetUserCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
