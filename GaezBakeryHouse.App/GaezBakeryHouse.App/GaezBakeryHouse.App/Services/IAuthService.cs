using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Services
{
    public interface IAuthService
    {
        [Post("/auth/Login")]
        Task<HttpResponseMessage> Login([Body]AuthRequestModel requestModel);
    }

    public class AuthService
    {
        readonly IAuthService _service;

        public AuthService() => _service = RestService.For<IAuthService>(Constants.Url);

        public async Task<bool> Login(AuthRequestModel requestModel)
        {
            try
            {
                var response = await _service.Login(requestModel);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var authResponseModel = JsonConvert.DeserializeObject<AuthResponseModel>(responseContent);

                    await SecureStorage.SetAsync("AccessToken", $"Bearer {authResponseModel.Token}");
                    await SecureStorage.SetAsync("ExpirationToken", authResponseModel.Expiration.ToString());
                    await SecureStorage.SetAsync("ApplicationUserId", authResponseModel.ApplicationUserId);

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
