using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using System.Collections.Generic;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers.Dtos;

public class PaginatedResponseTests
{
    [Fact]
    public void Ok_Should_Assign_All_Properties_Correctly()
    {
        var items = new List<string> { "item1", "item2" };
        var page = 2;
        var totalPages = 5;
        var totalCount = 50;

        var response = PaginatedResponse<string>.Ok(items, page, totalPages, totalCount);

        Assert.True(response.Success);
        Assert.Equal("Operação realizada com sucesso.", response.Message);
        Assert.Equal(items, response.Sales);
        Assert.Equal(page, response.CurrentPage);
        Assert.Equal(totalPages, response.TotalPages);
        Assert.Equal(totalCount, response.TotalCount);
    }

    [Fact]
    public void Ok_Should_Allow_Null_Sales()
    {
        var response = PaginatedResponse<string>.Ok(null, 1, 1, 0);

        Assert.Null(response.Sales);
    }
}
