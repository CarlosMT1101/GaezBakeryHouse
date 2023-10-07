using Acr.UserDialogs;
using GaezBakeryHouse.App.Interfaces;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel, IQueryAttributable, IRefresh
    {
        #region ATRIBUTES
        int _productId;
        int _productAmount = 1;
        readonly ProductService _productService;
        readonly ShoppingService _shoppingService;
        ProductModel _product;
        #endregion
        #region PROPERTIES
        public int ProductAmount
        {
            get => _productAmount;
            set
            {
                _productAmount = value;

                OnPropertyChanged();
                ((Command)OnIncrementAmountCommand).ChangeCanExecute();
                ((Command)OnDecrementAmountCommand).ChangeCanExecute();
            }
        }
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
        public ICommand OnIncrementAmountCommand { get; private set; }
        public ICommand OnDecrementAmountCommand { get; private set; }
        public ICommand OnAddToCartClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public ProductDetailViewModel()
        {
            Title = "Producto";
            _productService = new ProductService();
            _shoppingService = new ShoppingService();

            OnRefreshCommand = new Command(
               execute: async () => await LoadDataAsync(),
               canExecute: () => true);

            OnIncrementAmountCommand = new Command(
                execute: () => ProductAmount += 1,
                canExecute: () => ProductAmount < 100);

            OnDecrementAmountCommand = new Command(
                execute: () => ProductAmount -= 1,
                canExecute: () => ProductAmount > 1);

            OnAddToCartClickedCommand = new Command(
                execute: async () => await AddToCart(),
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
        private async Task AddToCart()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            var item = new ShoppingCartItemModel
            {
                ApplicationUserId = SecureStorage.GetAsync("ApplicationUserId").Result,
                Price = Product.Price,
                ProductId = Product.Id,
                Quantity = ProductAmount,
            };

            var addToCartSuccesfull = await _shoppingService.PostShoppingCartItem(item);

            if (addToCartSuccesfull)
            {
                await UserDialogs.Instance.AlertAsync("Producto agregado al carrito", "Mensaje", "Ok");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Algo salío mal", "Mensaje", "Ok");
            }

            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
