using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Tests.Setup;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Abi.DeveloperEvaluation.Infra;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Infra.Repositories;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Handlers;

public class GetSaleByIdHandlerTests : IClassFixture<WebApiTestFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly DefaultContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleByIdHandler> _logger;
    private readonly string _connectionString;

    public GetSaleByIdHandlerTests(WebApiTestFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<DefaultContext>();
        _mapper = _scope.ServiceProvider.GetRequiredService<IMapper>();
        _logger = _scope.ServiceProvider.GetRequiredService<ILogger<GetSaleByIdHandler>>();
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
    public async Task Handle_DeveRetornarVendaMapeada_QuandoEncontrada()
    {
        // Arrange
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "S999",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Teste",
            BranchId = Guid.NewGuid(),
            Items = []
        };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        var handler = new GetSaleByIdHandler(new SaleRepository(_context), _mapper, _logger);

        // Act
        var result = await handler.Handle(new GetSaleByIdQuery(sale.Id), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sale.Id, result.Id);
        Assert.Equal(sale.SaleNumber, result.SaleNumber);
    }
}
