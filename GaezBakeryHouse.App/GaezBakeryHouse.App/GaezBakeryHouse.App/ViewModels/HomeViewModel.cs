using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Utils;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        readonly ProductService _productService;
        readonly CategoryService _categoryService;
        public AwesomeObservableCollection<ProductModel> Products { get; private set; }
        public AwesomeObservableCollection<CategoryModel> Categories { get; private set; }

        public HomeViewModel()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();

            Products = new AwesomeObservableCollection<ProductModel>();
            Categories = new AwesomeObservableCollection<CategoryModel>();
        }

        public async Task LoadData()
        {
            Categories.AddRange(await _categoryService.GetAllCategories());

            foreach (var category in Categories)
            {
                category.ImageSource  = ImageSource.FromStream(() => new MemoryStream(category.Image));
            }
        }
    }
}
