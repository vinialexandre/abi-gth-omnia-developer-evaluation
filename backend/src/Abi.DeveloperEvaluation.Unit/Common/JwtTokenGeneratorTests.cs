using Xunit;
using Microsoft.Extensions.Configuration;
using Abi.DeveloperEvaluation.Common.Security;
using Moq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace Abi.DeveloperEvaluation.Unit.Common;
public class JwtTokenGeneratorTests
{
    private readonly JwtTokenGenerator _tokenGenerator;

    public JwtTokenGeneratorTests()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:SecretKey", "supersecretkeyforsigningjwtmustbe32bytes" }
            })
            .Build();

        _tokenGenerator = new JwtTokenGenerator(config);
    }

    [Fact]
    public void GenerateToken_Should_Return_Valid_JWT_With_Expected_Claims()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns("123");
        mockUser.Setup(u => u.Username).Returns("moqueca");
        mockUser.Setup(u => u.Role).Returns("admin");

        var token = _tokenGenerator.GenerateToken(mockUser.Object);

        Assert.False(string.IsNullOrWhiteSpace(token));

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);

        Assert.Equal("123", claims["nameid"]);        
        Assert.Equal("moqueca", claims["unique_name"]);
        Assert.Equal("admin", claims["role"]); 

        Assert.True(jwt.ValidTo > DateTime.UtcNow.AddHours(7.5));
    }


    [Fact]
    public void GenerateToken_Should_Throw_If_User_Is_Null()
    {
        Assert.Throws<NullReferenceException>(() => _tokenGenerator.GenerateToken(null!));
    }

    [Fact]
    public void GenerateToken_Should_Throw_If_Secret_Is_Missing()
    {
        var config = new ConfigurationBuilder().Build();
        var generator = new JwtTokenGenerator(config);

        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns("1");
        mockUser.Setup(u => u.Username).Returns("admin");
        mockUser.Setup(u => u.Role).Returns("admin");

        Assert.Throws<ArgumentNullException>(() => generator.GenerateToken(mockUser.Object));
    }
}
