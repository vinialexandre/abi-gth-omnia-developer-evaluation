using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.WebApi.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Abi.DeveloperEvaluation.Tests.UnitTests.WebApi.Common;

public class BaseControllerUnitTests
{
    private class FakeController : BaseController
    {
        public IActionResult CallOkResponse(string message, object? data = null) =>
            OkResponse(message, data);

        public IActionResult CallBadRequest(string message) =>
            BadRequest(message);

        public IActionResult CallNotFound(string message) =>
            NotFound(message);

        public IActionResult CallCreatedResponse(string action, object route, object? data = null) =>
            CreatedResponse(action, route, data);

        public IActionResult CallOkPaginated<T>(PaginatedList<T> list) =>
            OkPaginated(list);
    }

    private readonly FakeController _controller = new();

    [Fact]
    public void BadRequest_DeveRetornarApiResponseComStatus400()
    {
        var result = _controller.CallBadRequest("erro");

        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var response = badRequest.Value.Should().BeOfType<ApiResponse>().Subject;

        response.Success.Should().BeFalse();
        response.Message.Should().Be("erro");
    }

    [Fact]
    public void NotFound_DeveRetornarApiResponseComStatus404()
    {
        var result = _controller.CallNotFound("não encontrado");

        var notFound = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        var response = notFound.Value.Should().BeOfType<ApiResponse>().Subject;

        response.Success.Should().BeFalse();
        response.Message.Should().Be("não encontrado");
    }

    [Fact]
    public void OkResponse_DeveRetornarApiResponseComStatus200()
    {
        var result = _controller.CallOkResponse("ok", new { value = 123 });

        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = ok.Value.Should().BeOfType<ApiResponse>().Subject;

        response.Success.Should().BeTrue();
        response.Message.Should().Be("ok");
        response.Data.Should().NotBeNull();
    }

    [Fact]
    public void CreatedResponse_DeveRetornarApiResponseComStatus201()
    {
        var result = _controller.CallCreatedResponse("Get", new { id = 1 }, new { Name = "X" });

        var created = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        created.ActionName.Should().Be("Get");

        var response = created.Value.Should().BeOfType<ApiResponse>().Subject;
        response.Success.Should().BeTrue();
        response.Message.Should().Be("Criado com sucesso");
        response.Data.Should().NotBeNull();
    }

    [Fact]
    public void OkPaginated_DeveRetornarPaginatedResponse()
    {
        var items = new List<string> { "a", "b", "c" };
        var paged = new PaginatedList<string>(items, items.Count, 1, 10);

        var result = _controller.CallOkPaginated(paged);

        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = ok.Value.Should().BeOfType<PaginatedResponse<string>>().Subject;

        response.Sales.Should().BeEquivalentTo(items);
        response.CurrentPage.Should().Be(1);
        response.TotalPages.Should().Be(1);
        response.TotalCount.Should().Be(3);
    }

}
