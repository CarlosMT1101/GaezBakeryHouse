using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
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
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region CONSTRUCTOR
        public LoginViewModel() =>
            _service = new AuthService();
        #endregion
        #region FUNCTIONS                     
        #endregion
        #region COMMANDS
        public ICommand OnLoginClickedCommand => new Command(async () =>
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            var request = new AuthRequestModel { Email = this.Email, Password = this.Password };

            var isAuthenticate = await _service.Login(request);

            if (isAuthenticate)
            {
                await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Algo salió mal, intenta de nuevo", "Ok");
            }

            UserDialogs.Instance.HideLoading();
        });
        #endregion
    }
}
