using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region ATRIBUTES
        readonly CategoryService _categoryService;
        readonly OffertService _offertService;
        readonly ProductService _productService;
        #endregion
        #region PROPERTIES
        public ICommand OnRefreshCommand { get; private set; }
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
                execute: () => OnRefesh(),
                canExecute: () => true);
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
            var banners = _offertService.GetBanners();

            Banners.ClearRange();
            Banners.AddRange(banners);
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
        public async void OnRefesh() =>
            await LoadDataAsync();
        #endregion
    }
}
