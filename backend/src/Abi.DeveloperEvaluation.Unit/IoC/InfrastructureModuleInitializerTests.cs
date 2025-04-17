using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.IoC.ModuleInitializers;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Infra;
using Microsoft.EntityFrameworkCore;

namespace Abi.DeveloperEvaluation.Unit.IoC;

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
