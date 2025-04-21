using Abi.DeveloperEvaluation.Application;
using Abi.DeveloperEvaluation.Common.HealthChecks;
using Abi.DeveloperEvaluation.Common.Logging;
using Abi.DeveloperEvaluation.Common.Validation;
using Abi.DeveloperEvaluation.IoC;
using Abi.DeveloperEvaluation.Infra;
using Abi.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Abi.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateApp(args);
        app.Run();
    }

    public static WebApplication CreateApp(string[]? args = null)
    {
        var builder = WebApplication.CreateBuilder(args ?? []);

        builder.AddDefaultLogging();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.AddBasicHealthChecks();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DefaultContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Abi.DeveloperEvaluation.Infra")
            )
        );

        builder.RegisterDependencies();
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        var app = builder.Build();

        app.UseMiddleware<DomainExceptionMiddleware>();
        app.UseMiddleware<ValidationExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseBasicHealthChecks();
        app.MapControllers();

        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Migration");

        if (app.Environment.IsDevelopment())
        {
            MigrationInitializer.ApplyMigrations(app.Services, logger);
        }

        app.UseDefaultLogging();
        return app;
    }
}
