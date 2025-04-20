using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Abi.DeveloperEvaluation.Infra.Persistence;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Infra;

public class UnitOfWorkTests
{
    [Fact]
    public async Task CommitAsync_ShouldCall_SaveChangesAsync()
    {
        var mockContext = new Mock<DbContext>();
        mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        var uow = new UnitOfWork(mockContext.Object);
        await uow.CommitAsync();

        mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }
}
