using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GaezBakeryHouse.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _service;

        public AuthController(IAuthService service) =>
            _service = service;

        [HttpPost("Login")]
        public async Task<AuthResponseDTO> Login([FromBody] AuthRequestDTO request)
        {
            return await _service.Login(request);
        }

        [HttpPost("Register")]
        public async Task<RegistrationResponseDTO> Register([FromBody] RegistrationRequestDTO request)
        {
            return await _service.Register(request);
        }
    }
}
