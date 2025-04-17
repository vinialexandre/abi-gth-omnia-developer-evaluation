using Xunit;
using Abi.DeveloperEvaluation.Domain.Events;

namespace Abi.DeveloperEvaluation.Unit.Domain;

public class SaleCreatedEventTests
{
    [Fact]
    public void Should_Set_All_Properties_Correctly()
    {
        var saleId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var amount = 100.0m;

        var evt = new SaleCreatedEvent(saleId, date, customerId, amount);

        Assert.Equal(saleId, evt.SaleId);
        Assert.Equal(customerId, evt.CustomerId);
        Assert.Equal(date, evt.SaleDate);
        Assert.Equal(amount, evt.TotalAmount);
        Assert.True((DateTime.UtcNow - evt.OccurredOn).TotalSeconds < 2);
    }
}
