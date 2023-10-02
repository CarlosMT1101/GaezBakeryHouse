using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel, IQueryAttributable
    {
        #region ATRIBUTES
        int _productId;
        readonly ProductService _productService;
        ProductModel _product;
        #endregion
        #region PROPERTIES
        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                OnPropertyChanged();
            }
        }
        public ProductModel Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region COMMANDS
        public ICommand OnRefreshCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public ProductDetailViewModel()
        {
            Title = "Producto";
            _productService = new ProductService();

            OnRefreshCommand = new Command(
               execute: async () => await LoadDataAsync(),
               canExecute: () => true);
        }
        #endregion
        #region FUNCTIONS
        public void ApplyQueryAttributes(IDictionary<string, string> query) =>
            ProductId = int.Parse(HttpUtility.UrlDecode(query["id"]));
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadProduct();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        private async Task LoadProduct() =>
            Product = await _productService.GetProductById(ProductId);
        #endregion
    }
}
