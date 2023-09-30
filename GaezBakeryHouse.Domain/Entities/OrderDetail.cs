using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
