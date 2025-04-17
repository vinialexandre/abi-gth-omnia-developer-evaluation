using Xunit;
using Abi.DeveloperEvaluation.Domain.Events;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Events;

public class SaleCancelledEventTests
{
    [Fact]
    public void Should_Set_SaleId_And_Reason_Correctly()
    {
        var saleId = Guid.NewGuid();
        var reason = "Cliente desistiu";

        var evt = new SaleCancelledEvent(saleId, reason);

        Assert.Equal(saleId, evt.SaleId);
        Assert.Equal(reason, evt.Reason);
        Assert.True((DateTime.UtcNow - evt.CancelledAt).TotalSeconds < 2);
        Assert.True((DateTime.UtcNow - evt.OccurredOn).TotalSeconds < 2);
    }
}
