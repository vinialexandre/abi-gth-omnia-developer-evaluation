using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

namespace Ambev.DeveloperEvaluation.Common.HealthChecks;

/// <summary>
/// Provides extension methods for configuring health checks in an ASP.NET Core application.
/// </summary>
/// <remarks>
/// This class contains methods for both adding health checks to the service collection
/// and configuring health check endpoints in the application pipeline.
/// </remarks>
public static class HealthChecksExtension
{
    /// <summary>
    /// Adds basic health checks to the <see cref="HealthCheckService"/> in the application's service collection.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to add the health checks to.</param>
    /// <remarks>
    /// This method adds two basic health checks:
    /// <list type="bullet">
    /// <item>
    /// <description>"Liveness": A simple check that always returns healthy, tagged with "liveness".</description>
    /// </item>
    /// <item>
    /// <description>"Readiness": A simple check that always returns healthy, tagged with "readiness".</description>
    /// </item>
    /// </list>
    /// These checks can be used to verify the basic operational status of the application.
    /// </remarks>
    /// <example>
    /// This method can be used in Program.cs:
    /// <code>
    /// var builder = WebApplication.CreateBuilder(args);
    /// builder.AddBasicHealthChecks();
    /// </code>
    /// </example>
    public static void AddBasicHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck("Liveness", () => HealthCheckResult.Healthy(), tags: ["liveness"])
            .AddCheck("Readiness", () => HealthCheckResult.Healthy(), tags: ["readiness"]);
    }

    /// <summary>
    /// Configures and adds basic health check endpoints to the ASP.NET Core web application.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <remarks>
    /// This method sets up three health check endpoints:
    /// <list type="bullet">
    /// <item>
    /// <description>/health/live: For liveness probes, tagged with "liveness".</description>
    /// </item>
    /// <item>
    /// <description>/health/ready: For readiness probes, tagged with "readiness".</description>
    /// </item>
    /// <item>
    /// <description>/health: For overall health checks.</description>
    /// </item>
    /// </list>
    /// Each endpoint uses custom options configured by the WriteHealthCheckResponse method.
    /// </remarks>
    /// <example>
    /// This method can be used in the Configure method of Startup.cs or in Program.cs:
    /// <code>
    /// app.UseBasicHealthChecks();
    /// </code>
    /// </example>
    public static void UseBasicHealthChecks(this WebApplication app)
    {
        var livenessOptions = WriteHealtCheckRespose(app, "liveness");
        app.UseHealthChecks("/health/live", livenessOptions);

        var readinessOptions = WriteHealtCheckRespose(app, "readiness");
        app.UseHealthChecks("/health/ready", readinessOptions);

        var healthOptions = WriteHealtCheckRespose(app, string.Empty);
        app.UseHealthChecks("/health", healthOptions);

        var logger = app.Services.GetRequiredService<ILogger<HealthCheckService>>();
        logger.LogInformation("Health Check enabled at: '/health'");
    }

    /// <summary>
    /// Configures health check options for an ASP.NET Core web application.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    /// <param name="tag">The tag used to filter health checks.</param>
    /// <returns>A configured HealthCheckOptions object.</returns>
    /// <remarks>
    /// This method configures the following options:
    /// - Filters health checks based on the provided tag.
    /// - Sets specific HTTP status codes for different health states.
    /// - Configures a custom response writer that generates a JSON with detailed health status information.
    /// </remarks>
    private static HealthCheckOptions WriteHealtCheckRespose(this WebApplication app, string tag)
    {
        var options = new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains(tag),
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            ResponseWriter = async (context, report) =>
            {
                var result = new
                {
                    status = report.Status.ToString(),
                    healthChecks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        errorMessage = e.Value.Exception?.Message,
                        hostEnvironment = app.Environment.EnvironmentName.ToLowerInvariant()
                    }),
                };
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsJsonAsync(result);
            },
        };

        return options;
    }
}
