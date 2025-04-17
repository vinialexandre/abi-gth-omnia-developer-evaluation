using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using System;
using System.Collections.Generic;

namespace Abi.DeveloperEvaluation.Unit.Handlers.Dtos;

public class SaleResponseTests
{
    [Fact]
    public void Constructor_Should_Initialize_Defaults()
    {
        var response = new SaleResponse();

        Assert.NotNull(response.SaleNumber);
        Assert.NotNull(response.CustomerName);
        Assert.NotNull(response.BranchName);
        Assert.NotNull(response.Items);
        Assert.Equal(Guid.Empty, response.Id);
        Assert.Equal(0, response.TotalAmount);
    }

    [Fact]
    public void Should_Assign_And_Retrieve_Values()
    {
        var id = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var response = new SaleResponse
        {
            Id = id,
            SaleNumber = "123",
            SaleDate = date,
            CustomerName = "Cliente 1",
            BranchName = "Filial A",
            TotalAmount = 123.45m,
            Status = "Completed",
            Items = new List<SaleItemResponse> { new() { Quantity = 1, UnitPrice = 10 } }
        };

        Assert.Equal(id, response.Id);
        Assert.Equal("123", response.SaleNumber);
        Assert.Equal(date, response.SaleDate);
        Assert.Equal("Cliente 1", response.CustomerName);
        Assert.Equal("Filial A", response.BranchName);
        Assert.Equal(123.45m, response.TotalAmount);
        Assert.Equal("Completed", response.Status);
        Assert.Single(response.Items);
    }
}
