using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Unit.Utils;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Unit.Handlers;

public class GetAllSalesHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedSalesList()
    {
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                SaleNumber = "S001",
                CustomerName = "Cliente A",
                BranchName = "Filial A",
                Items = new List<SaleItem>()
            }
        };

        var saleResponses = new List<SaleResponse>
        {
            new() { SaleNumber = "S001", CustomerName = "Cliente A", BranchName = "Filial A" }
        };

        var repoMock = new Mock<ISaleRepository>();
        repoMock.Setup(r => r.Query()).Returns(new TestAsyncEnumerable<Sale>(sales));

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<SaleResponse>>(sales)).Returns(saleResponses);

        var loggerMock = new Mock<ILogger<GetAllSalesHandler>>();

        var handler = new GetAllSalesHandler(repoMock.Object, mapperMock.Object, loggerMock.Object);

        var result = await handler.Handle(new GetAllSalesQuery(), CancellationToken.None);

        Assert.Single(result);
        Assert.Equal("S001", result.First().SaleNumber);
    }
}
