using Acr.UserDialogs;
using GaezBakeryHouse.App.Interfaces;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel, IRefresh
    {
        #region ATTRIBUTES
        readonly OrderService _orderService;
        readonly ShoppingService _shoppingService;
        decimal _totalAmount;
        string _name;
        string _phoneNumber;
        string _address;
        #endregion
        #region PROPERTIES
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ((Command)OnOrderClickedCommand).ChangeCanExecute();
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
                ((Command)OnOrderClickedCommand).ChangeCanExecute();
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
                ((Command)OnOrderClickedCommand).ChangeCanExecute();
            }
        }
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region COMMANDS
        public ICommand OnOrderClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public OrderDetailViewModel()
        {
            Title = "Detalle De Orden";
            _orderService = new OrderService();
            _shoppingService = new ShoppingService();

            OnOrderClickedCommand = new Command(
                execute: async () => await Order(),
                canExecute: () => !(string.IsNullOrEmpty(Name) ||
                                    string.IsNullOrEmpty(PhoneNumber) ||
                                    string.IsNullOrEmpty(Address)));
        }
        #endregion
        #region FUNCTIONS
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            TotalAmount = await _shoppingService.GetUserTotalAmount();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        async Task Order()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            var order = new OrderModel
            {
                Address = Address,
                ApplicationUserId = await SecureStorage.GetAsync("ApplicationUserId"),
                FullName = Name,
                OrderTotal = TotalAmount,
                Phone = PhoneNumber
            };

            var isOrdered = await _orderService.PostOrder(order);

            if(isOrdered) 
            {
                var isDeleted = await _shoppingService.DeleteAllShoppingCartItemsByUserId();

                if (isDeleted)
                {
                    await UserDialogs.Instance.AlertAsync("Orden realizada con exíto", "Mensaje", "Ok");
                    await Shell.Current.GoToAsync("../");
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Ocurrío un error", "Error", "Ok");
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Ocurrío un error", "Error", "Ok");
            }

            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
