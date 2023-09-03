namespace GaezBakeryHouse.Application.DTOs
{
    public enum RegistrationResult
    {
        Sucess,
        Fail
    }

    public class RegistrationResponseDTO
    {
        public RegistrationResult Result { get; set; }
        public string Token { get; set; }
    }
}
