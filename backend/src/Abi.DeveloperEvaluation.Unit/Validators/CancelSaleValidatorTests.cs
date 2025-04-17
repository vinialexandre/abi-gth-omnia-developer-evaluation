using Xunit;
using Abi.DeveloperEvaluation.Application.Sales.Validators;
using Abi.DeveloperEvaluation.Application.Dtos;

namespace Abi.DeveloperEvaluation.Unit.Application.Validators;

public class UpdateSaleValidatorTests
{
    [Fact]
    public void Should_Fail_When_Items_Are_Empty()
    {
        var validator = new UpdateSaleValidator();
        var request = new SaleRequest
        {
            SaleNumber = "S001",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial",
            Items = new List<SaleItemRequest>(), 
            Status = "Completed"
        };

        var result = validator.Validate(request);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_With_Valid_Request()
    {
        var validator = new UpdateSaleValidator();
        var request = new SaleRequest
        {
            SaleNumber = "S001",
            SaleDate = DateTime.UtcNow.AddMinutes(-1), // <- Corrige comparação de tempo
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial",
            Items = new List<SaleItemRequest>
        {
            new() {
                ProductId = Guid.NewGuid(),
                ProductName = "Produto", // <- Certifique-se de preencher todos os campos
                Quantity = 5,
                UnitPrice = 10
            }
        },
            Status = "Completed"
        };

        var result = validator.Validate(request);
        Assert.True(result.IsValid);
    }

}
