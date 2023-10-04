using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views.CategorySelectedPageFolder;
using GaezBakeryHouse.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using GaezBakeryHouse.App.Views.ProductDetailPageFolder;
using GaezBakeryHouse.App.Interfaces;

namespace GaezBakeryHouse.App.ViewModels
{
    public class CategorySelectedViewModel : BaseViewModel, IQueryAttributable, IRefresh
    {
        #region ATRIBUTES
        readonly ProductService _productService;
        int _categoryId;
        #endregion
        #region PROPERTIES
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }
        public AwesomeObservableCollection<ProductModel> ProductsList { get; private set; }
        #endregion
        #region COMMANDS
        public ICommand OnProductClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public CategorySelectedViewModel()
        {
            _productService = new ProductService();
            ProductsList = new AwesomeObservableCollection<ProductModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnProductClickedCommand = new Command<ProductModel>(
                async (e) => await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}/{nameof(CategorySelectedPage)}/{nameof(ProductDetailPage)}?id={e.Id}"));
        }
        #endregion
        #region FUNCTIONS
        private async Task LoadProducts()
        {
            var products = await _productService.GetProductsByCategory(CategoryId);

            ProductsList.ClearRange();
            ProductsList.AddRange(products);
        }
        #endregion
        #region IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            CategoryId = int.Parse(HttpUtility.UrlDecode(query["id"]));
            Title = HttpUtility.UrlDecode(query["name"]);
        }
        #endregion
        #region IRefresh
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadProducts();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
