using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.IoC.ModuleInitializers;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Domain.DomainValidation;
using Abi.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Unit.Utils;

namespace Abi.DeveloperEvaluation.Unit.IoC;

public class ApplicationModuleInitializerTests
{
    [Fact]
    public void Should_Register_Validators_And_UnitOfWork()
    {
        var builder = WebApplication.CreateBuilder();

        var dbContextOptions = new DbContextOptionsBuilder<FakeContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        builder.Services.AddSingleton<DbContext>(new FakeContext(dbContextOptions));

        var initializer = new ApplicationModuleInitializer();
        initializer.Initialize(builder);

        var provider = builder.Services.BuildServiceProvider();

        Assert.NotNull(provider.GetService<IValidator<Sale>>());
        Assert.NotNull(provider.GetService<IValidator<SaleItem>>());
        Assert.NotNull(provider.GetService<IUnitOfWork>());
    }
}
