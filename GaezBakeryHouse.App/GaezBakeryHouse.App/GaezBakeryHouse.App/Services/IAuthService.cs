using GaezBakeryHouse.App.Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IAuthService
    {
        [Post("/auth/Login")]
        Task<HttpResponseMessage> Login([Body]AuthRequestModel request);
    }
}
