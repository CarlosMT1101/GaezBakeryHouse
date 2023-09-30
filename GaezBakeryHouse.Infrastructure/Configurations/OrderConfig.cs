using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GaezBakeryHouse.Infrastructure.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.FullName).IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Address).IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Phone).IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.OrderTotal).IsRequired()
                .HasPrecision(10, 2);

            builder.Property(x => x.IsOrderCompleted).IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.ApplicationUserId).IsRequired();
        }
    }
}
