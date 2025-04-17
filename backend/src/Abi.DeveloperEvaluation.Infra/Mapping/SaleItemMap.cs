using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abi.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Abi.DeveloperEvaluation.Infra.Mapping
{
    public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("sale_items");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductName).IsRequired();
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.Discount).HasColumnType("decimal(18,2)");
        }
    }
}
