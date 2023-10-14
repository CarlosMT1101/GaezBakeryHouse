using GaezBakeryHouse.App.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface ICategoryService
    {
        [Get("/category/GetAllCategories")]
        Task<IEnumerable<CategoryModel>> GetAllCategories([Header("Authorization")] string authorization);
    }
}
