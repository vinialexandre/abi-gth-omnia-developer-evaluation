using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Abi.DeveloperEvaluation.Infra;
using Moq;

namespace Abi.DeveloperEvaluation.Unit.IoC;

public class MigrationInitializerTests
{
    [Fact]
    public void Should_Ensure_Database_Created()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddDbContext<DefaultContext>(opt =>
            opt.UseInMemoryDatabase("MigrationTest"));

        var loggerMock = new Mock<ILogger>();

        var app = builder.Build();

        var exception = Record.Exception(() =>
            MigrationInitializer.ApplyMigrations(app.Services, loggerMock.Object, true));

        Assert.Null(exception);
    }
}
