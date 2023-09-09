using Azure;
using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<ActionResult> Login([FromBody] AuthRequestDTO request)
        {
            try
            {
                var response = await _service.Login(request);
                return StatusCode((int) HttpStatusCode.OK, response);
            }
            catch (Exception ex) 
            {
                var errorResponse = new ErrorReponseDTO { Message = "Algo salió mal" };
                return StatusCode((int) HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestDTO request)
        {
            try
            {
                var response = await _service.Register(request);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = "Algo salió mal" };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }  
        }
    }
}
