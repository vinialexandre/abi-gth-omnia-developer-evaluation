using System.Net;
using System.Net.Http.Json;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Common.Validation;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Tests.Setup;
using FluentAssertions;
using Xunit;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Controller;

[Collection("WebApi Collection")]
public class SaleControllerTests : IClassFixture<WebApiTestFactory>
{
    private readonly HttpClient _client;

    public SaleControllerTests(WebApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    private SaleRequest BuildSaleRequest(string saleNumber = "TEST123") => new()
    {
        SaleNumber = saleNumber,
        SaleDate = DateTime.UtcNow,
        CustomerId = Guid.NewGuid(),
        CustomerName = "Cliente X",
        BranchId = Guid.NewGuid(),
        Status = SaleStatus.Pending.ToString(),
        Items = new List<SaleItemRequest>
    {
        new()
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto Teste",
            Quantity = 1,
            UnitPrice = 10
        }
    }
    };


    [Fact]
    public async Task Create_DeveCriarVendaERetornarCreated()
    {
        var request = BuildSaleRequest();

        var response = await _client.PostAsJsonAsync("/api/sales", request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<SaleResponse>>();
        content!.Success.Should().BeTrue();
        content.Data.SaleNumber.Should().Be(request.SaleNumber);
    }

    [Fact]
    public async Task GetAll_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/sales");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
        content!.Success.Should().BeTrue();
    }

    [Fact]
    public async Task GetPaginated_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/sales/paginated?page=1&size=10");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_DeveRetornar404_SeNaoEncontrado()
    {
        var response = await _client.GetAsync($"/api/sales/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
        content!.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Update_DeveAtualizarCamposPermitidosERetornar200()
    {
        var create = BuildSaleRequest("UPDATE_TEST");

        var created = await _client.PostAsJsonAsync("/api/sales", create);
        var createdContent = await created.Content.ReadFromJsonAsync<ApiResponseWithData<SaleResponse>>();

        var update = new SaleRequest
        {
            SaleNumber = "UPDATED_SALE_NUMBER",
            SaleDate = create.SaleDate, 
            CustomerId = create.CustomerId, 
            CustomerName = "Cliente Alterado", 
            BranchId = create.BranchId,
            Status = SaleStatus.Completed.ToString(),
            Items = [] 
        };

        var response = await _client.PutAsJsonAsync($"/api/sales/{createdContent!.Data.Id}", update);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<SaleResponse>>();
        content.Should().NotBeNull();

        content!.Data.SaleNumber.Should().Be("UPDATED_SALE_NUMBER");
        content.Data.Status.Should().Be(SaleStatus.Completed.ToString());
        content.Data.CustomerName.Should().Be(create.CustomerName); // permanece igual
    }


    [Fact]
    public async Task Cancel_DeveRetornar200()
    {
        var create = BuildSaleRequest("CANCEL_TEST");

        var created = await _client.PostAsJsonAsync("/api/sales", create);
        var createdContent = await created.Content.ReadFromJsonAsync<ApiResponseWithData<SaleResponse>>();

        var cancel = new CancelSaleRequest("Teste cancelamento");
        var response = await _client.PostAsJsonAsync($"/api/sales/{createdContent!.Data.Id}/cancel", cancel);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private class ProblemDetailsWithValidation
    {
        public int Status { get; set; }
        public string Title { get; set; } = string.Empty;
        public Dictionary<string, string[]> Errors { get; set; } = [];
    }

    [Fact]
    public async Task Create_DeveRetornar400_SeRequisicaoInvalida()
    {
        var response = await _client.PostAsJsonAsync("/api/sales", new { });
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var raw = await response.Content.ReadAsStringAsync();
        Console.WriteLine(raw);

        var content = await response.Content.ReadFromJsonAsync<ProblemDetailsWithValidation>();
        content.Should().NotBeNull();
        content.Errors.Should().ContainKey("SaleNumber");
    }

   
}

