using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Abi.DeveloperEvaluation.Application.Sales.Handlers;
using Abi.DeveloperEvaluation.Application.Sales.Commands;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers;

public class CancelSaleHandlerTests
{
    private readonly Mock<ISaleRepository> _repoMock = new();
    private readonly Mock<ILogger<CancelSaleHandler>> _loggerMock = new();

    private CancelSaleHandler BuildHandler() => new(_repoMock.Object, _loggerMock.Object);

    [Fact]
    public async Task Handle_ValidCancel_ShouldUpdateAndCommit()
    {
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Status = SaleStatus.Completed
        };

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(sale);

        var command = new CancelSaleCommand(sale.Id, "Cliente desistiu");
        var handler = BuildHandler();

        var result = await handler.Handle(command, CancellationToken.None);

        _repoMock.Verify(r => r.UpdateAsync(sale), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        Assert.Equal(SaleStatus.Cancelled, sale.Status);
        Assert.Equal(MediatR.Unit.Value, result);
    }

    [Fact]
    public async Task Handle_NonExistingSale_ShouldThrow()
    {
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale?)null);

        var handler = BuildHandler();
        var command = new CancelSaleCommand(Guid.NewGuid(), "Qualquer motivo");

        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}
