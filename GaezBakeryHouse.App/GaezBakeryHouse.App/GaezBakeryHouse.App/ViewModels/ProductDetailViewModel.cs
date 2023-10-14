using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel, IQueryAttributable
    {
        #region Attributes
        private int _productId;
        private int _productAmount = 1;
        private readonly IProductService _productService;
        private readonly IShoppingCartItemService _shoppingService;
        private ProductModel _product;
        #endregion

        #region Properties
        public int ProductAmount
        {
            get => _productAmount;
            set
            {
                _productAmount = value;
                OnPropertyChanged();

                ((Command)OnDecrementAmountCommand).ChangeCanExecute();
                ((Command)OnIncrementAmountCommand).ChangeCanExecute();
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

        #region Commands
        public ICommand OnIncrementAmountCommand { get; private set; }

        public ICommand OnDecrementAmountCommand { get; private set; }

        public ICommand OnAddToCartClickedCommand { get; private set; }
        #endregion

        #region Constructor
        public ProductDetailViewModel()
        {
            Title = "Producto";
            _productService = RestService.For<IProductService>(Constants.Url);
            _shoppingService = RestService.For<IShoppingCartItemService>(Constants.Url);

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

        #region Functions
        public void ApplyQueryAttributes(IDictionary<string, string> query) =>
           ProductId = int.Parse(HttpUtility.UrlDecode(query["id"]));

        private async Task LoadProduct()
        {
            Product = await _productService.GetProductById(AccessToken, ProductId);
            Product.ImageSource = ImageSource.FromStream(() => new MemoryStream(Product.ProductImage));
        }
            
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                await LoadProduct();
                OnSuccessTask();
            }
            catch (Exception)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task AddToCart()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            try
            {
                var item = CreateShoppingCartItemModel();
                var response = await _shoppingService.PostShoppingCartItem(AccessToken, item);

                if (response.IsSuccessStatusCode)
                {
                    await UserDialogs.Instance.AlertAsync(
                        Constants.AddToCartMessage,
                        Constants.MessageTitle,
                        Constants.Ok);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync(
                        Constants.ErrorMessage,
                        Constants.ErrorTitle,
                        Constants.Ok);
            }

            UserDialogs.Instance.HideLoading();
        }

        private ShoppingCartItemModel CreateShoppingCartItemModel() =>
            new ShoppingCartItemModel
            {
                ApplicationUserId = ApplicationUserId,
                Price = Product.Price,
                ProductId = Product.Id,
                Quantity = ProductAmount
            };
        #endregion
    }
}
