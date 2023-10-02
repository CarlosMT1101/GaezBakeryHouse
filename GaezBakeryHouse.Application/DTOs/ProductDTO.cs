namespace GaezBakeryHouse.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage { get; set; }
        public decimal Price { get; set; }
        public bool IsTrendingProduct { get; set; }
        public int CategoryId { get; set; }
    }
}
