namespace GaezBakeryHouse.App.Models
{
    public class OrderModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
