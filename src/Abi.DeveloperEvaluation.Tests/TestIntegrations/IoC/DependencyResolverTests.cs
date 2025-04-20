using Xunit;
using Microsoft.AspNetCore.Builder;
using Abi.DeveloperEvaluation.IoC;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.IoC;

public class DependencyResolverTests
{
    [Fact]
    public void Should_Execute_All_Module_Initializers()
    {
        var builder = WebApplication.CreateBuilder();

        var exception = Record.Exception(() => DependencyResolver.RegisterDependencies(builder));

        Assert.Null(exception);
    }
}
