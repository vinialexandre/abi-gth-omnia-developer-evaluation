using Xunit;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Application.Sales.Validators;
using Abi.DeveloperEvaluation.Application.Dtos;
using System.Collections.Generic;
namespace Abi.DeveloperEvaluation.Unit.Handlers.Validators;
public class CreateSaleValidatorTests
{
    [Fact]
    public void Should_Fail_When_CustomerName_Is_Empty()
    {
        var request = new SaleRequest
        {
            CustomerName = "",
            Items = new List<SaleItemRequest> { new() { Quantity = 5 } }
        };
        var command = new CreateSaleCommand(request);

        var validator = new CreateSaleValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains("CustomerName"));
    }

    [Fact]
    public void Should_Fail_When_Quantity_Exceeds_Limit()
    {
        var request = new SaleRequest
        {
            CustomerName = "Xpto",
            Items = new List<SaleItemRequest> { new() { Quantity = 21 } }
        };
        var command = new CreateSaleCommand(request);

        var validator = new CreateSaleValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains("Quantity"));
    }

    [Fact]
    public void Should_Fail_When_Quantity_Is_Zero()
    {
        var request = new SaleRequest
        {
            CustomerName = "Xpto",
            Items = new List<SaleItemRequest> { new() { Quantity = 0 } }
        };
        var command = new CreateSaleCommand(request);

        var validator = new CreateSaleValidator();
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains("Quantity"));
    }

    [Fact]
    public void Should_Pass_With_Valid_Input()
    {
        var request = new SaleRequest
        {
            CustomerName = "Xpto",
            Items = new List<SaleItemRequest> { new() { Quantity = 2 } }
        };
        var command = new CreateSaleCommand(request);

        var validator = new CreateSaleValidator();
        var result = validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
