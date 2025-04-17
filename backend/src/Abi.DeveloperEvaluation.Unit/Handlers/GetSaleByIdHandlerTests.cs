using Xunit;
using Moq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Queries;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Unit.Utils;

namespace Abi.DeveloperEvaluation.Unit.Handlers;
public class GetSaleByIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedSale_WhenFound()
    {
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };

        var options = new DbContextOptionsBuilder<FakeContext>()
            .UseInMemoryDatabase("SaleByIdDb")
            .Options;

        using var context = new FakeContext(options);
        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(r => r.Query()).Returns(context.Sales.AsQueryable());

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<SaleResponse>(It.IsAny<Sale>()))
                  .Returns(new SaleResponse { Id = sale.Id });

        var handler = new GetSaleByIdHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetSaleByIdQuery(saleId), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(saleId, result.Id);
    }
}
