using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public byte[] CategoryImage { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
