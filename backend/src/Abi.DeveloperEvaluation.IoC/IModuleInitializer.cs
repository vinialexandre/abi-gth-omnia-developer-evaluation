using Microsoft.AspNetCore.Builder;

namespace Abi.DeveloperEvaluation.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
