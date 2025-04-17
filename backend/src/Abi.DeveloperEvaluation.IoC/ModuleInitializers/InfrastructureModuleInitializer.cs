using Abi.DeveloperEvaluation.Domain.Repositories;
using Abi.DeveloperEvaluation.Infra;
using Abi.DeveloperEvaluation.Infra.DependencyInjection;
using Abi.DeveloperEvaluation.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abi.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddInfrastructure();
    }
}