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
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] AuthRequestDTO request)
        {
            try
            {
                return await _service.Login(request);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponseDTO>> Register([FromBody] RegistrationRequestDTO request)
        {
            try
            {
                return await _service.Register(request);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
    }
}
