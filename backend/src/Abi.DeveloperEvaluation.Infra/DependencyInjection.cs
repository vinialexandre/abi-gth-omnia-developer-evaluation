using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Abi.DeveloperEvaluation.Infra.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISaleRepository, SaleRepository>();
        return services;
    }
}
