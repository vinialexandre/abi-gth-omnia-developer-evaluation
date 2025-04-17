using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Infra;
using Abi.DeveloperEvaluation.IoC.ModuleInitializers;
using Abi.DeveloperEvaluation.Domain.Repositories;


namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.IoC;

public class InfrastructureModuleInitializerTests
{
    [Fact]
    public void Should_Register_SaleRepository_And_DbContext()
    {
        var webBuilder = WebApplication.CreateBuilder();

        webBuilder.Services.AddDbContext<DefaultContext>(opt =>
            opt.UseInMemoryDatabase("TestDb"));

        var initializer = new InfrastructureModuleInitializer();
        initializer.Initialize(webBuilder);

        var provider = webBuilder.Services.BuildServiceProvider();

        Assert.NotNull(provider.GetService<ISaleRepository>());
        Assert.NotNull(provider.GetService<DbContext>());
    }
}
