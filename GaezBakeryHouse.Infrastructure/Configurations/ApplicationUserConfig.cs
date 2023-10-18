using GaezBakeryHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GaezBakeryHouse.Infrastructure.Configurations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
        }
    }
}
