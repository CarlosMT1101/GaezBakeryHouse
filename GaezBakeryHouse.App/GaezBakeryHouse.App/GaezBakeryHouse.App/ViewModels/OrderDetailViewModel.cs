using Acr.UserDialogs;
using GaezBakeryHouse.App.Data;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        #region Attributes
        private string _name;
        private readonly IOrderService _orderService;
        private string _phoneNumber;
        private readonly IShoppingCartItemService _shoppingService;
        private decimal _totalAmount;
        private string _address;
        #endregion

        #region Properties
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
        #endregion

        #region Commands
        public ICommand OnOrderClickedCommand { get; private set; }
        #endregion

        #region Constructor
        public OrderDetailViewModel()
        {
            Title = "Orden";

            _orderService = RestService.For<IOrderService>(Constants.Url);
            _shoppingService = RestService.For<IShoppingCartItemService>(Constants.Url);

            OnOrderClickedCommand = new Command(
                execute: async () => await Order(),
                canExecute: () =>
                {
                    return !(string.IsNullOrEmpty(Name) ||
                                    string.IsNullOrEmpty(PhoneNumber) ||
                                    string.IsNullOrEmpty(Address));
                });
        }
        #endregion

        #region Functions
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                await LoadTotalAmount();

                PhoneNumber = SecureStorage.GetAsync(Constants.PhoneNumber).Result;
                Name = $"{SecureStorage.GetAsync(Constants.FullName).Result} {SecureStorage.GetAsync(Constants.LastName).Result}"; 

                OnSuccessTask();
            }
            catch (Exception ex)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task Order()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                var orderModel = CreateOrderModel();

                var response = await _orderService.PostOrder(AccessToken, orderModel);

                if (response.IsSuccessStatusCode)
                {
                    var deletedResponse = await _shoppingService.DeleteAllShoppingCartItemsByUserId(AccessToken, ApplicationUserId);

                    if (deletedResponse.IsSuccessStatusCode)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            Constants.OrderMessage,
                            Constants.MessageTitle,
                            Constants.Ok);

                        await Shell.Current.GoToAsync("../");
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(
                            Constants.ErrorMessage,
                            Constants.ErrorTitle,
                            Constants.Ok);
                    }
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(
                            Constants.ErrorMessage,
                            Constants.ErrorTitle,
                            Constants.Ok);
                }

                OnSuccessTask();
            }
            catch (Exception ex)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task LoadTotalAmount()
        {
            var response = await _shoppingService.GetUserTotalAmount(AccessToken, ApplicationUserId);
            var responseContent = await response.Content.ReadAsStringAsync();

            TotalAmount = decimal.Parse(responseContent.Replace('.', ','));
        }

        private OrderModel CreateOrderModel() =>
            new OrderModel
            {
                Address = Address,
                ApplicationUserId = ApplicationUserId,
                FullName = Name,
                OrderTotal = TotalAmount,
                Phone = PhoneNumber
            };
        #endregion
    }
}
