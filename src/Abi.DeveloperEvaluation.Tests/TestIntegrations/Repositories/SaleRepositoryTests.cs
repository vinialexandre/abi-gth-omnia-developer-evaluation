using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Infra.Repositories;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Tests.Setup;
using Abi.DeveloperEvaluation.Infra;
using Microsoft.EntityFrameworkCore;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Infra;

public class SaleRepositoryTests : IClassFixture<WebApiTestFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly DefaultContext _context;
    private readonly ISaleRepository _repository;
    private readonly string _connectionString;

    public SaleRepositoryTests(WebApiTestFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<DefaultContext>();
        _repository = new SaleRepository(_context);
        _connectionString = _context.Database.GetConnectionString();
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync(); 
        await DatabaseCleaner.ResetDatabaseAsync(_connectionString); 
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task AddAsync_DevePersistirVenda()
    {
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" };

        await _repository.AddAsync(sale);
        await _repository.SaveChangesAsync();

        var result = await _context.Sales.FindAsync(sale.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarVenda()
    {
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(sale.Id);

        Assert.NotNull(result);
        Assert.Equal(sale.Id, result!.Id);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarTodasAsVendas()
    {
        _context.Sales.AddRange(
            new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" },
            new Sale { Id = Guid.NewGuid(), SaleNumber = "S002" }
        );
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ExistsAsync_DeveRetornarTrue_SeVendaExiste()
    {
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = "S001" };
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var exists = await _repository.ExistsAsync(sale.Id);

        Assert.True(exists);
    }

    [Fact]
    public async Task RemoveAllItems_DeveRemoverTodosOsItens()
    {
        var item1 = new SaleItem { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 10 };
        var item2 = new SaleItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 20 };

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "S001",
            Items = new List<SaleItem> { item1, item2 }
        };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        _repository.RemoveAllItems(sale);
        await _repository.SaveChangesAsync();

        var remaining = _context.SaleItems.ToList();
        Assert.Empty(remaining);
    }
}
