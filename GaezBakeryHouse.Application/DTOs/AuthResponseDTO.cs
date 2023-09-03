namespace GaezBakeryHouse.Application.DTOs
{
    public enum AuthResult
    {
        Sucess,
        InvalidCredentials
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public AuthResult Result { get; set; }
    }
}
