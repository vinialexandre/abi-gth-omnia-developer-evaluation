using Moq;
using AutoMapper;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Domain.Repositories;

namespace Abi.DeveloperEvaluation.Unit.Handlers;

public class UpdateSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _repoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    private UpdateSaleHandler BuildHandler() =>
        new(_repoMock.Object, _mapperMock.Object);

    [Fact]
    public async Task Handle_ValidUpdate_ShouldModifyAndReturnUpdatedSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();

        var existingSale = new Sale
        {
            Id = saleId,
            SaleNumber = "S123",
            Status = SaleStatus.Completed,
            Items = new List<SaleItem>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Produto Antigo", Quantity = 2, UnitPrice = 5 }
            }
        };

        var saleRequest = new SaleRequest
        {
            SaleNumber = "S999",
            Status = "Completed",
            Items = new List<SaleItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Produto Novo", Quantity = 10, UnitPrice = 3 }
            }
        };

        var updatedItems = new List<SaleItem>
        {
            new() { ProductId = saleRequest.Items[0].ProductId, ProductName = saleRequest.Items[0].ProductName, Quantity = 10, UnitPrice = 3 }
        };

        var saleResponse = new SaleResponse
        {
            Id = saleId,
            SaleNumber = saleRequest.SaleNumber,
            Status = "Completed",
            Items = new()
        };

        _repoMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(existingSale);
        _mapperMock.Setup(m => m.Map<List<SaleItem>>(saleRequest.Items)).Returns(updatedItems);
        _mapperMock.Setup(m => m.Map<SaleResponse>(It.IsAny<Sale>())).Returns(saleResponse);

        var handler = BuildHandler();

        var command = new UpdateSaleCommand(saleId, saleRequest);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.RemoveAllItems(existingSale), Times.Once);
        _repoMock.Verify(r => r.UpdateAsync(existingSale), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);

        Assert.Equal("S999", result.SaleNumber);
        Assert.Equal("Completed", result.Status);
    }

    [Fact]
    public async Task Handle_NonExistentSale_ShouldThrow()
    {
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale?)null);

        var handler = BuildHandler();
        var command = new UpdateSaleCommand(Guid.NewGuid(), new SaleRequest());

        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidStatus_ShouldThrow()
    {
        var sale = new Sale { Id = Guid.NewGuid() };
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(sale);

        var request = new SaleRequest { Status = "inexistente" };
        var handler = BuildHandler();
        var command = new UpdateSaleCommand(sale.Id, request);

        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}
