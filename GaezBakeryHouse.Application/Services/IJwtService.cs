using System.Security.Claims;

namespace GaezBakeryHouse.Application.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(ClaimsIdentity claimsIdentity);
    }
}
