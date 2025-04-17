using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Application.Sales.Validators;
namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers.Validators;
public class UpdateSaleValidatorTests
{
    [Fact]
    public void Should_Fail_If_SaleNumber_Is_Empty()
    {
        var dto = new SaleRequest
        {
            SaleNumber = "",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            Items = new List<SaleItemRequest> { new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 10 } }
        };

        var result = new UpdateSaleValidator().Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains("SaleNumber"));
    }

    [Fact]
    public void Should_Fail_If_Date_Is_Future()
    {
        var dto = new SaleRequest
        {
            SaleNumber = "123",
            SaleDate = DateTime.UtcNow.AddDays(1),
            CustomerId = Guid.NewGuid(),
            Items = new List<SaleItemRequest> { new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 10 } }
        };

        var result = new UpdateSaleValidator().Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains("SaleDate"));
    }

    [Fact]
    public void Should_Pass_With_Valid_Data()
    {
        var dto = new SaleRequest
        {
            SaleNumber = "123",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            Items = new List<SaleItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 }
            }
        };

        var result = new UpdateSaleValidator().Validate(dto);

        Assert.True(result.IsValid);
    }
}
