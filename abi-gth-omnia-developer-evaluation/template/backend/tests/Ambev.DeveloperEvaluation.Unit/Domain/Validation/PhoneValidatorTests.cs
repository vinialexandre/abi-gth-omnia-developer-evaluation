using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    /// <summary>
    /// Contains unit tests for the <see cref="PhoneValidator"/> class.
    /// Tests validation of phone numbers according to the following rules:
    /// - Must not be empty
    /// - Must match pattern: ^\+?[1-9]\d{1,14}$
    ///   (Optional '+' prefix, first digit 1-9, followed by 1-14 digits)
    /// </summary>
    public class PhoneValidatorTests
    {
        [Theory(DisplayName = "Given a phone number When validating Then should validate according to regex pattern")]
        [InlineData("+123456789", true)]      // Valid - with plus prefix
        [InlineData("123456789", true)]       // Valid - without plus prefix
        [InlineData("+551199999999", true)]   // Valid - longer number
        [InlineData("11999999999", true)]     // Valid - exactly 11 digits
        [InlineData("999999999", true)]       // Valid - 9 digits
        [InlineData("+0123456789", false)]    // Invalid - starts with 0 after plus
        [InlineData("0123456789", false)]     // Invalid - starts with 0
        [InlineData("+", false)]              // Invalid - only plus
        [InlineData("+12345678901234567", false)] // Invalid - too long (>15 digits with plus)
        [InlineData("12345678901234567", false)]  // Invalid - too long (>15 digits)
        [InlineData("abc12345678", false)]    // Invalid - contains letters
        [InlineData("12.34567890", false)]    // Invalid - contains special characters
        [InlineData("", false)]               // Invalid - empty
        public void Given_PhoneNumber_When_Validating_Then_ShouldValidateAccordingToPattern(string phone, bool expectedResult)
        {
            // Arrange
            var validator = new PhoneValidator();

            // Act
            var result = validator.Validate(phone);

            // Assert
            result.IsValid.Should().Be(expectedResult);
        }
    }
}
