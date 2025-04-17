using Xunit;
using Abi.DeveloperEvaluation.Application.Dtos;
using System;
using System.Collections.Generic;
using Abi.DeveloperEvaluation.Common.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Common;

public class UseBasicHealthChecksTests
{
    [Fact]
    public void AddJwtAuthentication_Should_Register_Auth_Services()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Jwt:SecretKey"] = "12345678901234567890123456789012"
            }).Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);
        services.AddJwtAuthentication(config);

        var provider = services.BuildServiceProvider();
        var result = provider.GetService<IJwtTokenGenerator>();

        Assert.NotNull(result);
    }

}
