using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GaezBakeryHouse.Infrastructure.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description).HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.ProductImage)
                .IsRequired(false);

            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasPrecision(10, 2);

            builder.Property(x => x.IsTrendingProduct)
                .IsRequired(true)
                .HasDefaultValue(false);

            builder.Property(x => x.CategoryId)
                .IsRequired(true);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.ProductId);

            builder.HasMany(x => x.ShoppingCartItems)
                .WithOne(x => x.Product)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
