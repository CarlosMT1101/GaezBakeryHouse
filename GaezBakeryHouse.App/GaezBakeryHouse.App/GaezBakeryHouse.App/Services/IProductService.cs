using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Services
{
    public interface IProductService
    {
        [Get("/products/GetProductsByCategory/{id}")]
        Task<HttpResponseMessage> GetProductsByCategory(int id, [Header("Authorization")] string authorizationToken);
    }

    public class ProductService
    {
        readonly IProductService _service;

        public ProductService() => _service = RestService.For<IProductService>(Constants.Url);


        public async Task<IEnumerable<ProductModel>> GetProductsByCategory(int id)
        {
            try
            {
                var authorizationToken = SecureStorage.GetAsync("AccessToken").Result;

                var response = await _service.GetProductsByCategory(id, authorizationToken);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var productsList = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(responseContent);

                    return productsList;
                }
                else
                {
                    return new List<ProductModel>();
                }
            }
            catch (Exception)
            {
                return new List<ProductModel>();
            }
        }
    }
}
