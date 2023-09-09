using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Models;
using GaezBakeryHouse.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Options;

namespace GaezBakeryHouse.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        readonly JwtSettings _jwtSettings;
        readonly UserManager<IdentityUser> _userManager;
        readonly IJwtService _jwtService;

        public AuthService(IOptions<JwtSettings> jwtSettings, 
                           UserManager<IdentityUser> userManager, 
                           IJwtService jwtService)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDTO> Login(AuthRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Si el usuario no existe o la contraseña es incorrecta
            if(user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new Exception();
            }

            var token = _jwtService.GenerateToken(user.Id);

            return new AuthResponseDTO
            {
                Expiration = DateTime.UtcNow.AddDays(1),
                Token = token,
            };
        }

        public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO request)
        {
            var emailExist = await _userManager.FindByEmailAsync(request.Email);
            var usernameExist = await _userManager.FindByNameAsync(request.UserName);

            var user = new IdentityUser
            { 
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            if(emailExist != null || usernameExist != null)
            {
                throw new Exception();
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token = _jwtService.GenerateToken(user.Id);

                return new RegistrationResponseDTO { Token =  token };
            }

            throw new Exception();
        }
    }
}
