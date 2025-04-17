using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;

namespace Abi.DeveloperEvaluation.Unit.Handlers.Dtos;

public class ApiResponseWithDataTests
{
    [Fact]
    public void Ok_Should_Return_Success_True_And_Data()
    {
        var result = ApiResponseWithData<string>.Ok("Moqueca");

        Assert.True(result.Success);
        Assert.Equal("Operação realizada com sucesso.", result.Message);
        Assert.Equal("Moqueca", result.Data);
    }

    [Fact]
    public void Ok_With_Custom_Message_Should_Set_Values_Correctly()
    {
        var result = ApiResponseWithData<int>.Ok(42, "Tudo certo");

        Assert.True(result.Success);
        Assert.Equal("Tudo certo", result.Message);
        Assert.Equal(42, result.Data);
    }

    [Fact]
    public void Fail_Should_Return_Success_False_And_Message()
    {
        var result = ApiResponseWithData<string>.Fail("Erro ao buscar");

        Assert.False(result.Success);
        Assert.Equal("Erro ao buscar", result.Message);
        Assert.Null(result.Data);
        Assert.NotNull(result.Errors);
    }
}
