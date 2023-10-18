using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Domain.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
