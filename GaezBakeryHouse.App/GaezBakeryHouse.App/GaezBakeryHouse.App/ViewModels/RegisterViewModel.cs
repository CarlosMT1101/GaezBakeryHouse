using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Attributes
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _userName;
        private string _phoneNumber;
        private readonly IAuthService _service;
        #endregion

        #region Properties
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

        #region Commands
        public ICommand OnRegisterClickedCommand { get; private set; }
        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            _service = RestService.For<IAuthService>(Constants.Url);

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

        #region Functions
        private async Task Register()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);

            try
            {
                var registerRequestModel = CreateRegisterRequestModel();
                var response = await _service.Register(registerRequestModel);

                if (response.IsSuccessStatusCode)
                {
                    await UserDialogs.Instance.AlertAsync(
                        Constants.RegisterMessage,
                        Constants.MessageTitle,
                        Constants.Ok);

                    await Shell.Current.GoToAsync("../");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                await UserDialogs.Instance.AlertAsync(
                        Constants.ErrorMessage,
                        Constants.ErrorTitle,
                        Constants.Ok);
            }

            UserDialogs.Instance.HideLoading();
        }

        private RegisterRequestModel CreateRegisterRequestModel() =>
            new RegisterRequestModel
            {
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber,
                UserName = UserName
            };
        #endregion
    }
}
