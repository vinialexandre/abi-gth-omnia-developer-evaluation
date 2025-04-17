using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Abi.DeveloperEvaluation.Infra.Repositories;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Infra.DependencyInjection;
using Abi.DeveloperEvaluation.Unit.Utils; 
using Microsoft.EntityFrameworkCore;

namespace Abi.DeveloperEvaluation.Unit.Infra;

public class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructure_Should_Register_ISaleRepository()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DbContext, FakeContext>(options =>
            options.UseInMemoryDatabase("InfraTestDb"));

        services.AddInfrastructure();

        var provider = services.BuildServiceProvider();
        var repo = provider.GetService<ISaleRepository>();

        Assert.NotNull(repo);
        Assert.IsType<SaleRepository>(repo);
    }
}
