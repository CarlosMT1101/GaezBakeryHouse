namespace GaezBakeryHouse.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string PhoneNumber { get; set; }
        public string ApplicationUserId { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
    }
}
