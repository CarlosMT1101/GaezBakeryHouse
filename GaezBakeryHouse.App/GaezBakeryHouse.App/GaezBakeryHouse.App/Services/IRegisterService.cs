using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Services
{
    public interface IRegisterService
    {
        [Post("/auth/Register")]
        Task<HttpResponseMessage> Register([Body] RegistrationRequestModel requestModel);
    }

    public class RegisterService
    {
        readonly IRegisterService _service;

        public RegisterService() => _service = RestService.For<IRegisterService>(Constants.Url);

        public async Task<bool> Register(RegistrationRequestModel requestModel)
        {
            try
            {
                var response = await _service.Register(requestModel);

                if (response.IsSuccessStatusCode)
                {
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
