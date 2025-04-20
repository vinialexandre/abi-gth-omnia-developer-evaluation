using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using Abi.DeveloperEvaluation.Common.Validation;
using System.Collections.Generic;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Handlers.Dtos;

public class ApiResponseTests
{
    [Fact]
    public void Ok_Should_Set_Success_True_And_Message()
    {
        var result = ApiResponse.Ok("Feito com sucesso", new { valor = 123 });

        Assert.True(result.Success);
        Assert.Equal("Feito com sucesso", result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void Ok_Should_Use_Default_Message_When_Not_Provided()
    {
        var result = ApiResponse.Ok();

        Assert.True(result.Success);
        Assert.Equal("Operação realizada com sucesso.", result.Message);
    }

    [Fact]
    public void Fail_Should_Set_Success_False_And_Message()
    {
        var result = ApiResponse.Fail("Falhou");

        Assert.False(result.Success);
        Assert.Equal("Falhou", result.Message);
        Assert.NotNull(result.Errors);
    }

    [Fact]
    public void Fail_Should_Include_Errors_When_Provided()
    {
        var errors = new List<ValidationErrorDetail>
        {
            new() { Error = "campo", Detail = "Obrigatório" }
        };

        var result = ApiResponse.Fail("Erro de validação", errors);

        Assert.False(result.Success);
        Assert.Equal("Erro de validação", result.Message);
        Assert.Single(result.Errors);
    }
}
