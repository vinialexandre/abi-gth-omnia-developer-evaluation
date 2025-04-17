using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using System;
using System.Collections.Generic;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers.Dtos;

public class SaleRequestTests
{
    [Fact]
    public void Properties_Should_Assign_Correctly()
    {
        var id = Guid.NewGuid();
        var items = new List<SaleItemRequest>
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50 }
        };

        var request = new SaleRequest
        {
            SaleNumber = "S-123",
            SaleDate = DateTime.UtcNow,
            CustomerId = id,
            CustomerName = "Moqueca Corp",
            BranchId = Guid.NewGuid(),
            Items = items
        };

        Assert.Equal("S-123", request.SaleNumber);
        Assert.Equal("Moqueca Corp", request.CustomerName);
        Assert.Equal(items, request.Items);
        Assert.Equal(id, request.CustomerId);
    }
}
