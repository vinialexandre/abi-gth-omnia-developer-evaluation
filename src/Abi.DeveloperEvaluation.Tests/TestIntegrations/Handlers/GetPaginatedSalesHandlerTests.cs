using Xunit;
using Moq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.Logging;
using Abi.DeveloperEvaluation.Tests.IntegrationTests.Utils;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Handlers;

public class GetPaginatedSalesHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WithMappedItems()
    {
        var options = new DbContextOptionsBuilder<FakeContext>()
            .UseInMemoryDatabase(databaseName: "SalesDb")
            .Options;

        using var context = new FakeContext(options);
        var sale = new Sale { Id = Guid.NewGuid() };
        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(r => r.GetQueryableAsync()).ReturnsAsync(context.Sales.AsQueryable());

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<List<SaleResponse>>(It.IsAny<List<Sale>>()))
                  .Returns(new List<SaleResponse> { new SaleResponse() });

        var mockLogger = new Mock<ILogger<GetPaginatedSalesHandler>>();

        var handler = new GetPaginatedSalesHandler(mockRepo.Object, mockMapper.Object, mockLogger.Object);
        var query = new GetPaginatedSalesQuery(1, 10);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(1, result.TotalPages);
    }
}
