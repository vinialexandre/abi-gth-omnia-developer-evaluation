using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using System;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers.Dtos;

public class SaleItemRequestTests
{
    [Fact]
    public void Properties_Should_Assign_Correctly()
    {
        var productId = Guid.NewGuid();
        var item = new SaleItemRequest
        {
            ProductId = productId,
            Quantity = 3,
            UnitPrice = 50
        };

        Assert.Equal(productId, item.ProductId);
        Assert.Equal(3, item.Quantity);
        Assert.Equal(50, item.UnitPrice);
    }
}
