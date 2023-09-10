using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Services
{
    public interface ICategoryService
    {
        [Get("/category/GetAllCategories")]
        Task<HttpResponseMessage> GetAllCategories([Header("Authorization")] string authorizationToken);
    }

    public class CategoryService
    {
        readonly ICategoryService _service;

        public CategoryService() => _service = RestService.For<ICategoryService>(Constants.Url);

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            try
            {
                var authorizationToken = SecureStorage.GetAsync("AccessToken").Result;

                var response = await _service.GetAllCategories(authorizationToken);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var categoriesList = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(responseContent);

                    return categoriesList;
                }
                else
                {
                    return new List<CategoryModel>();
                }
            }
            catch (Exception)
            {
                return new List<CategoryModel>();
            }
        }
    }
}
