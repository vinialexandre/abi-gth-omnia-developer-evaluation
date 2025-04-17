using Abi.DeveloperEvaluation.Domain.Entities;
using Abi.DeveloperEvaluation.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Abi.DeveloperEvaluation.Tests.IntegrationTests.Utils
{
    public class FakeContext : DbContext
    {
        public FakeContext(DbContextOptions<FakeContext> options) : base(options) { }

        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseEvent>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
