using Xunit;
using Microsoft.AspNetCore.Builder;
using Abi.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.IoC;

public class WebApiModuleInitializerTests
{
    [Fact]
    public void Should_Register_ControllerOptions()
    {
        var builder = WebApplication.CreateBuilder();
        var initializer = new WebApiModuleInitializer();

        initializer.Initialize(builder);

        var provider = builder.Services.BuildServiceProvider();

        var options = provider.GetService<IOptions<MvcOptions>>();
        Assert.NotNull(options);
    }
}
