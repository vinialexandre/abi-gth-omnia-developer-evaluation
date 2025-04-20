using Abi.DeveloperEvaluation.Domain.DomainValidation;
using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Infra.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Abi.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<IValidator<Sale>, SaleValidator>();
        services.AddScoped<IValidator<SaleItem>, SaleItemValidator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
