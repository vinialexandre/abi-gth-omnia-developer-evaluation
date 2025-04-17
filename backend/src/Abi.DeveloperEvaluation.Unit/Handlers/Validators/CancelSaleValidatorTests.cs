using Xunit;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Application.Sales.Validators;
using System;

namespace Abi.DeveloperEvaluation.Unit.Handlers.Validators;

public class CancelSaleValidatorTests
{
    [Fact]
    public void Should_Fail_If_Id_Is_Empty()
    {
        var command = new CancelSaleCommand(Guid.Empty, "Motivo válido");
        var validator = new CancelSaleValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "SaleId");
    }

    [Fact]
    public void Should_Fail_If_Reason_Is_Empty()
    {
        var command = new CancelSaleCommand(Guid.NewGuid(), "");
        var validator = new CancelSaleValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Reason");
    }

    [Fact]
    public void Should_Fail_If_Reason_Is_Short()
    {
        var command = new CancelSaleCommand(Guid.NewGuid(), "abc");
        var validator = new CancelSaleValidator();

        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Reason");
    }

    [Fact]
    public void Should_Pass_With_Valid_Data()
    {
        var command = new CancelSaleCommand(Guid.NewGuid(), "Motivo completo");
        var validator = new CancelSaleValidator();

        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
