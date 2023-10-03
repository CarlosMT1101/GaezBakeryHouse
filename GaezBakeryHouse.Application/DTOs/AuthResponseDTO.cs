namespace GaezBakeryHouse.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string ApplicationUserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
