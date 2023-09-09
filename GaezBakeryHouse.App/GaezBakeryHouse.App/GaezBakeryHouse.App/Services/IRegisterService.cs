using GaezBakeryHouse.App.Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IRegisterService
    {
        [Post("/auth/Register")]
        Task<HttpResponseMessage> Register([Body] RegistrationRequestModel request);
    }
}
