using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Interfaces;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using GaezBakeryHouse.App.Views.CategorySelectedPageFolder;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class HomeViewModel : BaseViewModel, IRefresh
    {
        #region ATRIBUTES
        readonly CategoryService _categoryService;
        readonly OffertService _offertService;
        readonly ProductService _productService;
        #endregion
        #region PROPERTIES
        public ICommand OnCategoryClicked { get; private set; }
        public AwesomeObservableCollection<CategoryModel> CategoriesList { get; private set; }
        public AwesomeObservableCollection<ProductModel> TrendingProductsList { get; private set; }
        public AwesomeObservableCollection<OffertModel> Banners { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public HomeViewModel()
        {
            _categoryService = new CategoryService();
            _offertService = new OffertService();
            _productService = new ProductService();
            CategoriesList = new AwesomeObservableCollection<CategoryModel>();
            TrendingProductsList = new AwesomeObservableCollection<ProductModel>();
            Banners = new AwesomeObservableCollection<OffertModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnCategoryClicked = new Command<CategoryModel>(
                execute: async (e) => await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}/{nameof(CategorySelectedPage)}?id={e.Id}&name={e.Name}"));
        }
        #endregion
        #region FUNCTIONS
        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategories();

            CategoriesList.ClearRange();
            CategoriesList.AddRange(categories);
        }
        private void LoadBanners()
        {
            if(Banners.Count == 0)
            {
                Banners.AddRange(_offertService.GetBanners());
            }
        }
        private async Task LoadTrendingProducts()
        {
            var products = await _productService.GetTrendingProducts();

            TrendingProductsList.ClearRange();
            TrendingProductsList.AddRange(products);
        }
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadCategoriesAsync();
            await LoadTrendingProducts();
            LoadBanners();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
