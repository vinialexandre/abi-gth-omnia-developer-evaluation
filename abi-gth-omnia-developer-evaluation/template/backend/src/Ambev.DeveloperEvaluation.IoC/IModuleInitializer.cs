using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperEvaluation.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
