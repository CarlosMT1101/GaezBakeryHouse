using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Newtonsoft.Json;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        string _email;
        string _password;
        readonly AuthService _service;
        #endregion
        #region PROPERTIES
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ((Command)OnLoginClickedCommand).ChangeCanExecute();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ((Command)OnLoginClickedCommand).ChangeCanExecute();
            }
        }
        #endregion
        #region COMMANDS
        public ICommand OnLoginClickedCommand { get; private set; }
        public ICommand OnRegisterClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public LoginViewModel()
        {
            _service = new AuthService();

            OnLoginClickedCommand = new Command(
                execute: async () => await Login(), 
                canExecute: () => !(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password)));

            OnRegisterClickedCommand = new Command(
                execute: async () => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegisterPage)}"),
                canExecute: () => true);
        }
        #endregion
        #region FUNCTIONS            
        async Task Login()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            var requestModel = new AuthRequestModel
            { 
                Email = Email, 
                Password = Password 
            };

            var isAuthenticate =  await _service.Login(requestModel);

            if (isAuthenticate)
            {
                await Shell.Current.GoToAsync($"//Start/{ nameof(HomePage) }");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Algo salió mal", "Error");
            }

            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
