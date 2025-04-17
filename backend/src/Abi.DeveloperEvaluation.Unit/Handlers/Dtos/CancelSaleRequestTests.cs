using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;

namespace Abi.DeveloperEvaluation.Unit.Handlers.Dtos;
public class CancelSaleRequestTests
{
    [Fact]
    public void Should_Assign_Reason_Correctly()
    {
        var reason = "Cancelado por duplicidade";

        var request = new CancelSaleRequest(reason);

        Assert.Equal(reason, request.Reason);
    }
}
