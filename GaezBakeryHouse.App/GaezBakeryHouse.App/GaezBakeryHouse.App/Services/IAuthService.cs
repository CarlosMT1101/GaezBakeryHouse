using GaezBakeryHouse.App.Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IAuthService
    {
        [Post("/auth/Login")]
        Task<AuthResponseModel> Login([Body] AuthRequestModel requestModel);

        [Post("/auth/Register")]
        Task<HttpResponseMessage> Register([Body] RegisterRequestModel requestModel);
    }
}
