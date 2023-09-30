using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.Infrastructure.Configurations
{
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.ProductId)
               .IsRequired();
        }
    }
}
