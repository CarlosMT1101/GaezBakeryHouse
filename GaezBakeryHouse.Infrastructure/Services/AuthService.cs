using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Services;

namespace GaezBakeryHouse.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public Task<AuthResponseDTO> Login(AuthRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResponseDTO> Register(RegistrationRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
