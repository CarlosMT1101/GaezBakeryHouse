using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IAuthService
    {
        Task<bool> Login(AuthRequestModel request);
    }

    public class AuthService : IAuthService
    {
        HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"{Constants.Url}/auth/Login");
        }

        public async Task<bool> Login(AuthRequestModel request)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_httpClient.BaseAddress, content);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
    }
}
