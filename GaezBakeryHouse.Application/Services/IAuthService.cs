using GaezBakeryHouse.Application.DTOs;

namespace GaezBakeryHouse.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> Login(AuthRequestDTO request);

        Task<RegistrationResponseDTO> Register(RegistrationRequestDTO request);
    }
}
