using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.DTOs
{
    public class ShoppingCartItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public byte[] ProductImage { get; set; }
        public string ProductName { get; set; }
    }
}
