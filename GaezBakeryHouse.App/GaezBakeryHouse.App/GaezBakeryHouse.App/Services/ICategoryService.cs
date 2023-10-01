using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Services
{
    public interface ICategoryService
    {
        [Get("/category/GetAllCategories")]
        Task<HttpResponseMessage> GetAllCategories([Header("Authorization")] string authorization);
    }

    public class CategoryService
    {
        readonly ICategoryService _categoryService;

        public CategoryService() =>
            _categoryService = RestService.For<ICategoryService>(Constants.Url);

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var response = await _categoryService.GetAllCategories(accessToken);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(responseContent);
                    
                    categories = categories.OrderBy(x => x.Name);

                    foreach(var category in categories)
                    {
                        category.ImageSource = ImageSource.FromStream(() => new MemoryStream(category.CategoryImage));
                    }

                    return categories;
                }
                else
                {
                    return new List<CategoryModel>();
                }
            }
            catch(Exception ex)
            {
                return new List<CategoryModel>();
            }
        }
    }
}
