namespace GaezBakeryHouse.App.Models
{
    public class UserResponseModel
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string LastName { get; set; }

        public string ApplicationUserId { get; set; }

        public string Token { get; set; }

        public string UserName { get; set; }

        public DateTime Expiration { get; set; }
    }
}
