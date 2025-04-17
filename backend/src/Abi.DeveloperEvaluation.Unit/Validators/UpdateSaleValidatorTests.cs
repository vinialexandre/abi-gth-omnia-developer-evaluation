using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Validators;
using FluentValidation;
using Abi.DeveloperEvaluation.Application.Sales.Commands;

namespace Abi.DeveloperEvaluation.Unit.Application.Validators
{
    public class CancelSaleValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        public void Should_Fail_Invalid_Reason(string reason)
        {
            var validator = new CancelSaleValidator();
            var result = validator.Validate(new CancelSaleCommand(Guid.NewGuid(), reason));
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_With_Valid_Reason()
        {
            var validator = new CancelSaleValidator();
            var result = validator.Validate(new CancelSaleCommand(Guid.NewGuid(), "Pedido duplicado"));
            Assert.True(result.IsValid);
        }
    }
}
