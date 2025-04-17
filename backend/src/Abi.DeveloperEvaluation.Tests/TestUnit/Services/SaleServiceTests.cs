using Xunit;
using Moq;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Application.Sales;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Domain.DomainValidation;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Services;

public class SaleServiceTests
{
    private readonly Mock<ISaleRepository> _repoMock = new();
    private readonly Mock<IValidator<Sale>> _saleValidator = new();
    private readonly Mock<IValidator<SaleItem>> _itemValidator = new();

    private SaleService BuildService() =>
        new SaleService(_repoMock.Object, _saleValidator.Object, _itemValidator.Object);

    private Sale CreateSampleSale()
    {
        return new Sale
        {
            SaleNumber = "S001",
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente Teste",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial Teste",
            Items = new List<SaleItem>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Produto 1", Quantity = 5, UnitPrice = 10 }
            }
        };
    }

    [Fact]
    public async Task CreateAsync_ValidSale_ShouldAddAndPublishEvents()
    {
        var sale = CreateSampleSale();
        var service = BuildService();

        var result = await service.CreateAsync(sale);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        Assert.Equal(SaleStatus.Completed, result.Status);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task UpdateAsync_ValidSale_ShouldUpdateAndPublishEvents()
    {
        var sale = CreateSampleSale();
        sale.Status = SaleStatus.Completed;

        var service = BuildService();

        var result = await service.UpdateAsync(sale);

        _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Sale>()), Times.Once);
        Assert.Equal(SaleStatus.Completed, result.Status);
    }

    [Fact]
    public async Task CancelAsync_ExistingSale_ShouldSetStatusToCancelled()
    {
        var sale = CreateSampleSale();

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(sale);

        var service = BuildService();

        await service.CancelAsync(Guid.NewGuid(), "Motivo de teste");

        _repoMock.Verify(r => r.UpdateAsync(It.Is<Sale>(s => s.Status == SaleStatus.Cancelled)), Times.Once);
        Assert.Equal(SaleStatus.Cancelled, sale.Status);
    }

    [Fact]
    public async Task CancelAsync_NonExistingSale_ShouldThrowDomainException()
    {
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Sale?)null);

        var service = BuildService();

        await Assert.ThrowsAsync<DomainException>(() =>
            service.CancelAsync(Guid.NewGuid(), "Inexistente"));
    }
}
