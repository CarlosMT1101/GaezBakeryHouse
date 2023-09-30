using GaezBakeryHouse.Domain.Common;

namespace GaezBakeryHouse.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool IsOrderCompleted { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
