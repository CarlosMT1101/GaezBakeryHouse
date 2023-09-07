using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using System.Windows.Input;
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
        #region COMMANDS
        public ICommand OnLoginClickedCommand => new Command(async () =>
        {
            var request = new AuthRequestModel
            {
                Email = _email,
                Password = _password
            };

            var response = await _service.Login(request);
        });
        #endregion
    }
}
