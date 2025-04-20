using Moq;
using AutoMapper;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers;

public class CreateSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<CreateSaleHandler>> _loggerMock = new();

    private CreateSaleHandler BuildHandler() =>
        new(_repoMock.Object, _uowMock.Object, _mapperMock.Object, _loggerMock.Object);

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddSaleAndCommit()
    {
        var saleRequest = new SaleRequest
        {
            SaleNumber = "S001",
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente Teste",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial Teste",
            Items = new()
            {
                new SaleItemRequest
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto",
                    Quantity = 5,
                    UnitPrice = 10
                }
            }
        };

        var saleEntity = new Sale
        {
            SaleNumber = saleRequest.SaleNumber,
            CustomerId = saleRequest.CustomerId,
            CustomerName = saleRequest.CustomerName,
            BranchId = saleRequest.BranchId,
            BranchName = saleRequest.BranchName,
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = saleRequest.Items[0].ProductId,
                    ProductName = saleRequest.Items[0].ProductName,
                    Quantity = 5,
                    UnitPrice = 10
                }
            }
        };

        var saleResponse = new SaleResponse
        {
            Id = Guid.NewGuid(),
            SaleNumber = saleRequest.SaleNumber,
            CustomerName = saleRequest.CustomerName,
            BranchName = saleRequest.BranchName,
            TotalAmount = 50,
            Items = new()
        };

        _mapperMock.Setup(m => m.Map<Sale>(saleRequest)).Returns(saleEntity);
        _mapperMock.Setup(m => m.Map<SaleResponse>(It.IsAny<Sale>())).Returns(saleResponse);

        var handler = BuildHandler();

        var result = await handler.Handle(new CreateSaleCommand(saleRequest), CancellationToken.None);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        Assert.Equal("S001", result.SaleNumber);
    }
}
