using GaezBakeryHouse.Domain.Entities;

namespace GaezBakeryHouse.Application.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
