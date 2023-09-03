using GaezBakeryHouse.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GaezBakeryHouse.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> Login(AuthRequestDTO request);

        Task<RegistrationResponseDTO> Register(RegistrationRequestDTO request);
    }
}
