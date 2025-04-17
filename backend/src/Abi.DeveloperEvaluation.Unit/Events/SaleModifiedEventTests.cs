using Xunit;
using Abi.DeveloperEvaluation.Domain.Events;
using Abi.DeveloperEvaluation.Domain.Enums;

namespace Abi.DeveloperEvaluation.Unit.Domain;

public class SaleModifiedEventTests
{
    [Fact]
    public void Should_Set_Properties_Correctly()
    {
        var saleId = Guid.NewGuid();
        var saleNumber = "S001";
        var modifiedAt = DateTime.UtcNow;
        var status = SaleStatus.Completed;

        var evt = new SaleModifiedEvent(saleId, saleNumber, modifiedAt, status);

        Assert.Equal(saleId, evt.SaleId);
        Assert.Equal(saleNumber, evt.SaleNumber);
        Assert.Equal(modifiedAt, evt.ModifiedAt);
        Assert.Equal(status, evt.Status);
        Assert.True((DateTime.UtcNow - evt.OccurredOn).TotalSeconds < 2);
    }
}
