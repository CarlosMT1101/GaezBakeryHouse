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
        private string _email;
        private string _password;
        private IAuthService _service;
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
            _service = RestService.For<IAuthService>(Constants.Url);

            OnLoginClickedCommand = new Command(
                execute: async () =>
                {
                    UserDialogs.Instance.ShowLoading("Cargando");

                    try
                    {
                        var request = new AuthRequestModel { Email = this.Email, Password = this.Password };
                        var response = await _service.Login(request);
                        AuthResponseModel responseContent;

                        if (response.IsSuccessStatusCode)
                        {
                            responseContent = JsonConvert.DeserializeObject<AuthResponseModel>(await response.Content.ReadAsStringAsync());
                            await SaveToken(responseContent);
                            await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}");
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        await UserDialogs.Instance.AlertAsync("Algo salió mal", "Error", "Ok");
                    }

                    UserDialogs.Instance.HideLoading();
                },
                canExecute: () =>
                {
                    return !(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password));
                });

            OnRegisterClickedCommand = new Command(
                execute: async () =>
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegisterPage)}");
                });
        }
            
        #endregion
        #region FUNCTIONS   
        private async Task SaveToken(AuthResponseModel responseContent)
        {
            await SecureStorage.SetAsync("AccessToken", responseContent.Token);
            await SecureStorage.SetAsync("ExpirationToken", responseContent.Expiration.ToString());
        }
        private void Login()
        {

        }
        #endregion
    }
}
