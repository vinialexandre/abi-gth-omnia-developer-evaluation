using Xunit;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Domain.Events;
using Abi.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abi.DeveloperEvaluation.Unit.Domain;

public class SaleTests
{
    [Fact]
    public void Cancel_DeveMarcarComoCanceladoEAdicionarEvento()
    {
        var sale = BuildValidSale();
        sale.Cancel("Cliente desistiu");

        Assert.Equal(SaleStatus.Cancelled, sale.Status);
        Assert.Contains(sale.DomainEvents, e => e is SaleCancelledEvent);
    }

    [Fact]
    public void Cancel_SeJaCancelada_DeveLancarExcecao()
    {
        var sale = BuildValidSale();
        sale.Cancel("Primeira vez");

        Assert.Throws<DomainException>(() => sale.Cancel("Repetindo"));
    }

    [Fact]
    public void MarkAsCompleted_SemItens_DeveLancarExcecao()
    {
        var sale = new Sale();

        Assert.Throws<DomainException>(() => sale.MarkAsCompleted());
    }

    [Fact]
    public void Modify_ComStatusCancelado_DeveLancarExcecao()
    {
        var sale = BuildValidSale();
        sale.Cancel("Cancelando");

        var updatedItems = new List<SaleItem>
        {
            new() { ProductId = Guid.NewGuid(), ProductName = "Novo", Quantity = 5, UnitPrice = 10 }
        };

        Assert.Throws<DomainException>(() => sale.Modify("S123", updatedItems, SaleStatus.Completed));
    }

    [Fact]
    public void Modify_DeveSubstituirItensEAdicionarEvento()
    {
        var sale = BuildValidSale();

        var updatedItems = new List<SaleItem>
        {
            new() { ProductId = Guid.NewGuid(), ProductName = "Novo Produto", Quantity = 5, UnitPrice = 10 }
        };

        sale.Modify("S456", updatedItems, SaleStatus.Completed);

        Assert.Single(sale.Items);
        Assert.Equal("Novo Produto", sale.Items.First().ProductName);
        Assert.Contains(sale.DomainEvents, e => e is SaleModifiedEvent);
    }

    private Sale BuildValidSale()
    {
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "S001",
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial"
        };

        sale.AddItem(new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto",
            Quantity = 5,
            UnitPrice = 10
        });

        return sale;
    }
}
