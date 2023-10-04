using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Interfaces;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel, IRefresh
    {
        #region ATRIBUTES
        readonly ShoppingService _shoppingService;
        decimal _totalAmount;
        #endregion
        #region PROPERTIES
        public AwesomeObservableCollection<ShoppingCartItemModel> ShoppingCartItemsList { get; private set; }
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
                ((Command)OnOrderClickedCommand).ChangeCanExecute();
            }
        }
        #endregion
        #region COMMANDS
        public ICommand OnDeleteClickedCommand { get; private set; }
        public ICommand OnOrderClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public ShoppingCartViewModel()
        {
            Title = "Carrito";
            _shoppingService = new ShoppingService();
            ShoppingCartItemsList = new AwesomeObservableCollection<ShoppingCartItemModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnDeleteClickedCommand = new Command<ShoppingCartItemModel>(
                execute: async (e) => await DeleteShoppingCartItem(e.Id, e.ProductId));

            OnOrderClickedCommand = new Command(
                execute: async () => await Order(),
                canExecute: () => ShoppingCartItemsList.Count() > 0);
        }
        #endregion
        #region FUNCTIONS
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadShoppingCartItems();
            TotalAmountProducts();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        private async Task LoadShoppingCartItems()
        {
            var shopItems = await _shoppingService.GetShoppingCartItemsByUserId();

            ShoppingCartItemsList.ClearRange();
            ShoppingCartItemsList.AddRange(shopItems);
        }
        private void TotalAmountProducts()
        {
            TotalAmount = 0M;

            foreach(var product in ShoppingCartItemsList)
            {
                TotalAmount += product.TotalAmount;
            }
        }
        private async Task DeleteShoppingCartItem(int id, int productId)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;

            var isDeleted = await _shoppingService.DeleteShoppingCartItem(id, productId);

            if(isDeleted)
            {
                await LoadShoppingCartItems();
                TotalAmountProducts();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Ocurrío un error", "Mensaje", "Ok");
            }

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        } 
        private async Task Order()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;

            var isOrdered = await _shoppingService.DeleteAllShoppingCartItemsByUserId();
            await LoadShoppingCartItems();
            TotalAmountProducts();

            if(isOrdered)
            {
                await UserDialogs.Instance.AlertAsync("Orden realizada con exíto", "Mensaje", "Ok");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Ocurrío un error", "Mensaje", "Ok");
            }

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
