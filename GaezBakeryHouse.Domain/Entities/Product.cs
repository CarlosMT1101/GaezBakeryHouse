using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage { get; set; }
        public decimal Price { get; set; }
        public bool IsTrendingProduct { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
