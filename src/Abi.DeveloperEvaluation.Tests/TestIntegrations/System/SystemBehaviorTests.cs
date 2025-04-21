using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Domain.Enums;
using Abi.DeveloperEvaluation.Infra;
using Abi.DeveloperEvaluation.Tests.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Abi.DeveloperEvaluation.Tests.TestIntegrations.System;

[Collection("WebApi Collection")]
public class SystemBehaviorTests : IClassFixture<WebApiTestFactory>
{
    private readonly HttpClient _client;

    public SystemBehaviorTests(WebApiTestFactory factory)
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
            new() { ProductId = Guid.NewGuid(), ProductName = "Produto Teste", Quantity = 1, UnitPrice = 10 }
        }
    };


    [Fact]
    public async Task Middleware_DeveTratarValidationExceptionERetornar400()
    {
        var response = await _client.PostAsJsonAsync("/api/sales", new { });
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("errors").And.Contain("SaleNumber");
    }

    [Fact]
    public async Task Middleware_DeveTratarDomainExceptionERetornar400ComMensagem()
    {
        var invalidRequest = BuildSaleRequest();
        invalidRequest.SaleNumber = ""; 

        var response = await _client.PostAsJsonAsync("/api/sales", invalidRequest);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);

        if (doc.RootElement.TryGetProperty("success", out var successProp))
        {
            successProp.GetBoolean().Should().BeFalse();
            doc.RootElement.GetProperty("message").GetString().Should().NotBeNull()
                .And.Match(msg =>
                    msg.Contains("obrigatório", StringComparison.OrdinalIgnoreCase)
                    || msg.Contains("erro", StringComparison.OrdinalIgnoreCase));
        }
        else
        {
            doc.RootElement.GetProperty("title").GetString().Should().NotBeNull();
            doc.RootElement.GetProperty("status").GetInt32().Should().Be(400);
        }
    }




    [Fact]
    public async Task HealthCheck_DeveRetornarHealthy()
    {
        var response = await _client.GetAsync("/health");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Match(x => x.Contains("Healthy") || x.Contains("UP"));
    }

    [Fact]
    public void DbContextFactory_DeveCriarContexto()
    {
        var factory = new YourDbContextFactory();
        var context = factory.CreateDbContext([]);

        context.Should().NotBeNull();
        context.Should().BeOfType<DefaultContext>();
    }
}
