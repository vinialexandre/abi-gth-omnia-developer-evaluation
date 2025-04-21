using Xunit;
using Microsoft.Extensions.Logging;
using Abi.DeveloperEvaluation.Infra;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Tests.Setup;
using Moq;
using Abi.DeveloperEvaluation.IoC.ModuleInitializers;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.IoC;

[Collection("WebApi Collection")]
public class MigrationInitializerTests : IClassFixture<WebApiTestFactory>
{
    private readonly WebApiTestFactory _factory;

    public MigrationInitializerTests(WebApiTestFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void ApplyMigrations_DeveExecutarSemExcecao()
    {
        // Arrange
        var logger = new Mock<ILogger>().Object;
        var serviceProvider = _factory.Services;

        // Act
        var exception = Record.Exception(() =>
            MigrationInitializer.ApplyMigrations(serviceProvider, logger, true));

        // Assert
        Assert.Null(exception);
    }
}
