using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Newtonsoft.Json;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        string _email;
        string _password;
        string _confirmPassword;
        string _userName;
        string _phoneNumber;
        readonly RegisterService _service;
        #endregion
        #region PROPERTIES
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
                ((Command)OnRegisterClickedCommand).ChangeCanExecute();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                ((Command)OnRegisterClickedCommand).ChangeCanExecute();
            }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                ((Command)OnRegisterClickedCommand).ChangeCanExecute();
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
                ((Command)OnRegisterClickedCommand).ChangeCanExecute();
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set 
            {
                _phoneNumber = value;
                OnPropertyChanged();
                ((Command)OnRegisterClickedCommand).ChangeCanExecute();
            }
        }
        #endregion
        #region COMMANDS
        public ICommand OnBackButtonClickedCommand { get; private set; }
        public ICommand OnRegisterClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public RegisterViewModel()
        {
            _service = new RegisterService();

            OnBackButtonClickedCommand = new Command(
                execute: async () => await Shell.Current.GoToAsync($"../"),
                canExecute: () => true);

            OnRegisterClickedCommand = new Command(
                execute: async () => await Register(),
                canExecute: () =>
                {
                    // Si los campos no están vacios y la contraseña es igual a lo que esta
                    // en confirmar contraseña, regresa true, de lo contrario false

                    return !(string.IsNullOrWhiteSpace(Email) || 
                             string.IsNullOrWhiteSpace(Password) ||
                             string.IsNullOrWhiteSpace(ConfirmPassword) || 
                             string.IsNullOrWhiteSpace(UserName) ||
                             string.IsNullOrWhiteSpace(PhoneNumber)) &&
                             Password.Equals(ConfirmPassword);
                });
        }
        #endregion
        #region FUNCTIONS
        async Task Register()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            var requestModel = new RegistrationRequestModel
            {
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber,
                UserName = UserName,
            };

            var registrationSuccess = await _service.Register(requestModel);

            if (registrationSuccess)
            {
                await UserDialogs.Instance.AlertAsync("Regístro exitoso", "Mensaje", "Ok");
                await Shell.Current.GoToAsync($"../");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Algo salió mal", "Error", "Ok");
            }

            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
