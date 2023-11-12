using GaezBakeryHouse.App.Models;
using Refit;

namespace GaezBakeryHouse.App.Services
{
    public interface IAuthService
    {
        [Post("/auth/Login")]
        Task<AuthResponseModel> Login([Body] AuthRequestModel authRequestModel);

        [Post("/auth/Register")]
        Task<HttpResponseMessage> Register([Body] RegisterRequestModel requestModel);
    }
}
