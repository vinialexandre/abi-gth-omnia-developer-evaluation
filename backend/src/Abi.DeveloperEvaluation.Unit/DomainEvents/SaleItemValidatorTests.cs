using Xunit;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.DomainValidation;

namespace Abi.DeveloperEvaluation.Unit.DomainEvents;

public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator = new();

    [Fact]
    public void Validate_ItemComQuantidadeInvalida_DeveLancarExcecao()
    {
        var item = new SaleItem { Quantity = 0, UnitPrice = 10 };

        Assert.Throws<DomainException>(() => _validator.Validate(item));
    }

    [Fact]
    public void Validate_ItemComPrecoInvalido_DeveLancarExcecao()
    {
        var item = new SaleItem { Quantity = 5, UnitPrice = 0 };

        Assert.Throws<DomainException>(() => _validator.Validate(item));
    }

    [Fact]
    public void Validate_ItemValido_NaoDeveLancarExcecao()
    {
        var item = new SaleItem { Quantity = 5, UnitPrice = 10 };

        _validator.Validate(item); 
    }
}
