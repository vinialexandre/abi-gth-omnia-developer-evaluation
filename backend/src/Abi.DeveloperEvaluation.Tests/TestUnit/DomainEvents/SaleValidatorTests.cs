using Xunit;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.DomainValidation;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.DomainEvents;

public class SaleValidatorTests
{
    private readonly SaleValidator _validator = new();

    [Fact]
    public void Validate_SemNomeCliente_DeveLancarExcecao()
    {
        var sale = new Sale
        {
            CustomerName = "",
            Items = new List<SaleItem> { new() { Quantity = 1, UnitPrice = 10 } }
        };

        Assert.Throws<DomainException>(() => _validator.Validate(sale));
    }

    [Fact]
    public void Validate_SemItens_DeveLancarExcecao()
    {
        var sale = new Sale
        {
            CustomerName = "Cliente",
            Items = new List<SaleItem>()
        };

        Assert.Throws<DomainException>(() => _validator.Validate(sale));
    }

    [Fact]
    public void Validate_TotalZero_DeveLancarExcecao()
    {
        var sale = new Sale
        {
            CustomerName = "Cliente",
            Items = new List<SaleItem> { new() { Quantity = 1, UnitPrice = 0 } }
        };

        Assert.Throws<DomainException>(() => _validator.Validate(sale));
    }

    [Fact]
    public void Validate_SaleValida_NaoDeveLancarExcecao()
    {
        var sale = new Sale
        {
            CustomerName = "Cliente",
            Items = new List<SaleItem> { new() { Quantity = 2, UnitPrice = 10 } }
        };

        _validator.Validate(sale); 
    }
}
