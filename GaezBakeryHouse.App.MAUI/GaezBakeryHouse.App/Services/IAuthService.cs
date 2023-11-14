using GaezBakeryHouse.App.Models;
using Refit;

namespace GaezBakeryHouse.App.Services
{
    public interface IAuthService
    {
        [Post("/auth/Login")]
        Task<UserResponseModel> Login([Body] UserModel authRequestModel);

        [Post("/auth/Register")]
        Task<HttpResponseMessage> Register([Body] RegisterRequestModel requestModel);
    }

    public class AuthService
    {
        private readonly IAuthService _authService;

        public AuthService() =>
            _authService = RestService.For<IAuthService>(Constants.Url);

        public async Task<bool> Login(UserModel userModel)
        {
            try
            {
                var response = await _authService.Login(userModel);

                if(response != null)
                {
                    App.IsUserLoggedIn = true;
                    App.SaveUserInformation(response);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
