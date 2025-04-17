using Xunit;
using Abi.DeveloperEvaluation.Common.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Abi.DeveloperEvaluation.Unit.Common;
public class LoggingExtensionTests
{
    [Fact]
    public void AddDefaultLogging_Should_Register_ILoggerFactory()
    {
        var builder = WebApplication.CreateBuilder();

        builder.AddDefaultLogging();

        var provider = builder.Services.BuildServiceProvider();
        var loggerFactory = provider.GetService<ILoggerFactory>();

        Assert.NotNull(loggerFactory);
        var logger = loggerFactory.CreateLogger("TestLogger");
        Assert.NotNull(logger);
    }

    [Fact]
    public void AddDefaultLogging_Should_Register_Logging()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        var provider = services.BuildServiceProvider();
        var logger = provider.GetService<ILoggerFactory>();

        Assert.NotNull(logger);
    }

}
