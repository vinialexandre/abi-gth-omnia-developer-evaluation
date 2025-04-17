using Xunit;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Infra.Repositories;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Unit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abi.DeveloperEvaluation.Unit.Infra;

public class SaleRepositoryTests
{
    private FakeContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<FakeContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new FakeContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldAddSale()
    {
        var context = CreateDbContext("AddSaleDb");
        var repo = new SaleRepository(context);

        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" };
        await repo.AddAsync(sale);
        await repo.SaveChangesAsync();

        var count = await context.Sales.CountAsync();
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnSale()
    {
        var context = CreateDbContext("GetByIdDb");
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" };
        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        var repo = new SaleRepository(context);
        var result = await repo.GetByIdAsync(sale.Id);

        Assert.NotNull(result);
        Assert.Equal(sale.Id, result!.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSales()
    {
        var context = CreateDbContext("GetAllDb");
        context.Sales.AddRange(
            new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" },
            new Sale { Id = Guid.NewGuid(), SaleNumber = "S002" }
        );
        await context.SaveChangesAsync();

        var repo = new SaleRepository(context);
        var result = await repo.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenSaleExists()
    {
        var context = CreateDbContext("ExistsDb");
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" };
        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        var repo = new SaleRepository(context);
        var exists = await repo.ExistsAsync(sale.Id);

        Assert.True(exists);
    }

    [Fact]
    public async Task RemoveAllItems_ShouldRemoveItemsFromSale()
    {
        var context = CreateDbContext("RemoveItemsDb");

        var item1 = new SaleItem { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 10 };
        var item2 = new SaleItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 20 };

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "S001",
            Items = new List<SaleItem> { item1, item2 }
        };

        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        var repo = new SaleRepository(context);
        repo.RemoveAllItems(sale);
        await repo.SaveChangesAsync();

        var remaining = context.SaleItems.ToList();
        Assert.Empty(remaining);
    }
}
