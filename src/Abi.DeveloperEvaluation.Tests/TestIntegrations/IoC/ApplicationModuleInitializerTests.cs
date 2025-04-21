using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.IoC.ModuleInitializers;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Domain.DomainValidation;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Tests.Setup;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.IoC;

public class ApplicationModuleInitializerTests : IClassFixture<WebApiTestFactory>
{
    private readonly IServiceProvider _provider;

    public ApplicationModuleInitializerTests(WebApiTestFactory factory)
    {
        _provider = factory.Services.CreateScope().ServiceProvider;
    }

    [Fact]
    public void DeveResolverValidadoresEUnitOfWork()
    {
        // Act
        var validatorVenda = _provider.GetService<IValidator<Sale>>();
        var validatorItem = _provider.GetService<IValidator<SaleItem>>();
        var unitOfWork = _provider.GetService<IUnitOfWork>();

        // Assert
        Assert.NotNull(validatorVenda);
        Assert.NotNull(validatorItem);
        Assert.NotNull(unitOfWork);
    }
}
