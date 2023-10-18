using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GaezBakeryHouse.Infrastructure.Configurations
{
    public class ShoppingCartItemConfig : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.ApplicationUserId)
                .IsRequired();
        }
    }
}
