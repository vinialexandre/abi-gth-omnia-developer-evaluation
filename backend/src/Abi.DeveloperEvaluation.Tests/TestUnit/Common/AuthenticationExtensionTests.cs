using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Abi.DeveloperEvaluation.Common.Security;
using System.Collections.Generic;

namespace Abi.DeveloperEvaluation.Tests.TestUnit.Common;

public class AuthenticationExtensionTests
{
    [Fact]
    public void AddJwtAuthentication_Should_Configure_IJwtTokenGenerator()
    {
        var services = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:SecretKey", "thisisaverylongsecretkeyforjwt123456" }
            }).Build();

        services.AddSingleton<IConfiguration>(config); 

        services.AddJwtAuthentication(config);
        var provider = services.BuildServiceProvider();

        var generator = provider.GetService<IJwtTokenGenerator>();
        Assert.NotNull(generator);
        Assert.IsType<JwtTokenGenerator>(generator);
    }
}
