using Xunit;
using Abi.DeveloperEvaluation.Common.Security;

namespace Abi.DeveloperEvaluation.Unit.Common;
public class BCryptPasswordHasherTests
{
    [Fact]
    public void HashPassword_And_VerifyPassword_Should_Work()
    {
        var hasher = new BCryptPasswordHasher();
        var plainPassword = "Moqueca123!";

        var hashed = hasher.HashPassword(plainPassword);
        var isMatch = hasher.VerifyPassword(plainPassword, hashed);

        Assert.True(isMatch);
        Assert.NotEqual(plainPassword, hashed);
    }

    [Fact]
    public void VerifyPassword_Should_ReturnFalse_When_Invalid()
    {
        var hasher = new BCryptPasswordHasher();

        var result = hasher.VerifyPassword("senhaerrada", "$2a$11$123456789012345678901u0Z0w1e2r3t4y5u6i7o8p9q0");

        Assert.False(result);
    }
}
