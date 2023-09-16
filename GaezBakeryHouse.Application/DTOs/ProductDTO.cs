namespace GaezBakeryHouse.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }

        public decimal Discount { get; set; }

        public bool InOffer { get; set; }
    }
}
