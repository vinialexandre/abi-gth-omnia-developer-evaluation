using Ambev.DeveloperEvaluation.Infra;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


public static class MigrationInitializer
{
    public static void ApplyMigrations(IServiceProvider serviceProvider, ILogger logger)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        if (db.Database.CanConnect())
        {
            db.Database.Migrate();
            logger.LogInformation("Migrations aplicadas com sucesso.");
        }
        else
        {
            logger.LogWarning("Não foi possível conectar ao banco. Migrations não aplicadas.");
        }
    }
}