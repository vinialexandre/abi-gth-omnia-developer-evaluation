using Abi.DeveloperEvaluation.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Abi.DeveloperEvaluation.IoC.ModuleInitializers;
public static class MigrationInitializer
{
    public static void ApplyMigrations(IServiceProvider serviceProvider, ILogger logger, bool isTest = false)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(logger);

        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        try
        {
            db.Database.Migrate();
            logger.LogInformation("Migração concluída com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao aplicar migrações.");
            throw;
        }
    }

}
