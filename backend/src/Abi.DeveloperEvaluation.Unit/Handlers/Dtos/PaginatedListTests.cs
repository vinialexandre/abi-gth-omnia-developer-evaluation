using Abi.DeveloperEvaluation.Application.Dtos;
using Xunit;
using System.Collections.Generic;
using System.Linq;
namespace Abi.DeveloperEvaluation.Unit.Handlers.Dtos;
public class PaginatedListTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        var items = new List<string> { "item1", "item2" };
        int count = 10;
        int page = 2;
        int pageSize = 2;

        var paginated = new PaginatedList<string>(items, count, page, pageSize);

        Assert.Equal(page, paginated.CurrentPage);
        Assert.Equal(pageSize, paginated.PageSize);
        Assert.Equal(count, paginated.TotalCount);
        Assert.Equal(5, paginated.TotalPages);
        Assert.True(paginated.HasPrevious);
        Assert.True(paginated.HasNext);
        Assert.Equal(items, paginated);
    }

    [Fact]
    public void WithNewItems_ShouldRetainPaginationMetadata()
    {
        var items = new List<string> { "a", "b" };
        var paginated = new PaginatedList<string>(items, 10, 1, 2);

        var newItems = new List<int> { 1, 2 };

        var result = paginated.WithNewItems(newItems);

        Assert.Equal(paginated.CurrentPage, result.CurrentPage);
        Assert.Equal(paginated.PageSize, result.PageSize);
        Assert.Equal(paginated.TotalCount, result.TotalCount);
        Assert.Equal(newItems, result);
    }
}
