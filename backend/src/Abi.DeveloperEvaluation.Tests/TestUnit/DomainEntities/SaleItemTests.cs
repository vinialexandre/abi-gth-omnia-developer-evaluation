using Xunit;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Domain.Events;
using Abi.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.DomainEntities;

public class SaleItemTests
{
    [Theory]
    [InlineData(3, 10, 0)]         // < 4 sem desconto
    [InlineData(5, 10, 5)]         // 10% de 50 = 5
    [InlineData(10, 10, 20)]       // 20% de 100 = 20
    public void ApplyDiscount_DeveAplicarDescontoCorretamente(int quantity, decimal unitPrice, decimal expectedDiscount)
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto",
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        item.ApplyDiscount();

        Assert.Equal(expectedDiscount, item.Discount);
    }

    [Fact]
    public void ApplyDiscount_QuantidadeAcimaDe20_DeveLancarExcecao()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto",
            Quantity = 21,
            UnitPrice = 10
        };

        Assert.Throws<DomainException>(() => item.ApplyDiscount());
    }
}
