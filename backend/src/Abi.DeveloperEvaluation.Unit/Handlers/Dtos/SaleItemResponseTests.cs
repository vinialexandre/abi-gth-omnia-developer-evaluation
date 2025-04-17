using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using System;

namespace Abi.DeveloperEvaluation.Unit.Handlers.Dtos;

public class SaleItemResponseTests
{
    [Fact]
    public void Constructor_Should_Initialize_Defaults()
    {
        var item = new SaleItemResponse();

        Assert.Equal(Guid.Empty, item.ProductId);
        Assert.Equal(0, item.Quantity);
        Assert.Equal(0, item.UnitPrice);
        Assert.Equal(0, item.Discount);
        Assert.Equal(0, item.Total);
    }

    [Fact]
    public void Should_Assign_Values()
    {
        var item = new SaleItemResponse
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = 50,
            Discount = 5,
            Total = 95
        };

        Assert.Equal(2, item.Quantity);
        Assert.Equal(50, item.UnitPrice);
        Assert.Equal(5, item.Discount);
        Assert.Equal(95, item.Total);
    }
}
