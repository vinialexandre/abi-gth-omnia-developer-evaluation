using Abi.DeveloperEvaluation.WebApi;
using Abi.DeveloperEvaluation.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Abi.DeveloperEvaluation.Tests.Setup;

public class WebApiTestFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DefaultConnection"] = "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
                })
                .Build();

            configBuilder.AddConfiguration(configuration);
        });
    }
    public async Task MigrateDatabaseAsync()
    {
        using var scope = Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        await context.Database.MigrateAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        var configuration = Services.GetRequiredService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        await DatabaseCleaner.ResetDatabaseAsync(connectionString);
    }
}
