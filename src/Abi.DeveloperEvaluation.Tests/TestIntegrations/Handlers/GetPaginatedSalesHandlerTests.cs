using System.Diagnostics;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Infra.Repositories;
using Abi.DeveloperEvaluation.Tests.Setup;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Abi.DeveloperEvaluation.Infra;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using MediatR;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Handlers;

public class GetPaginatedSalesHandlerTests : IClassFixture<WebApiTestFactory>, IAsyncLifetime
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetPaginatedSalesHandler> _logger;
    private WebApiTestFactory _factory;

    public GetPaginatedSalesHandlerTests(WebApiTestFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();

        await _factory.MigrateDatabaseAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Handle_DeveRetornarListaPaginadaComMapeamentoCorreto()
    {
        using var scope = _factory.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<GetPaginatedSalesQuery, PaginatedList<SaleResponse>>>();

        var sale1 = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "S001",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente A",
            BranchId = Guid.NewGuid(),
            Items = []
        };

        var sale2 = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "S002",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente B",
            BranchId = Guid.NewGuid(),
            Items = []
        };

        context.Sales.AddRange(sale1, sale2);
        await context.SaveChangesAsync();

        var query = new GetPaginatedSalesQuery(Page: 1, Size: 10);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(2, result.Count());
    }
   
}
