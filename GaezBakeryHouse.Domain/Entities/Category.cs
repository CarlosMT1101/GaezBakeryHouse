using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Domain.Entities;

public partial class Category : BaseEntity
{
    public string Name { get; set; }

    public byte[] Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
