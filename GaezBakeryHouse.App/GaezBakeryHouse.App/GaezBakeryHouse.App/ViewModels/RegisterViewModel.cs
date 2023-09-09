using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Newtonsoft.Json;
using Refit;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _userName;
        private string _phoneNumber;
        readonly IRegisterService _service;
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
            _service = RestService.For<IRegisterService>(Constants.Url);

            OnBackButtonClickedCommand = new Command(
                execute: async () =>
                {
                    await Shell.Current.GoToAsync($"../");
                });

            OnRegisterClickedCommand = new Command(
                execute: async () =>
                {
                    UserDialogs.Instance.ShowLoading("Cargando");

                    try
                    {
                        var request = new RegistrationRequestModel 
                        { 
                            Email = this.Email, 
                            Password = this.Password,
                            PhoneNumber = this.PhoneNumber,
                            UserName = this.UserName, 
                        };

                        var response = await _service.Register(request);

                        if (response.IsSuccessStatusCode)
                        {
                            await UserDialogs.Instance.AlertAsync("Regístro exítoso", "Mensaje", "Ok");
                            await Shell.Current.GoToAsync($"../");
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
                    // Si los campos no están vacios y la contraseña es igual a lo que esta
                    // en confirmar contraseña

                    return !(string.IsNullOrWhiteSpace(Email) || 
                             string.IsNullOrWhiteSpace(Password) ||
                             string.IsNullOrWhiteSpace(ConfirmPassword) || 
                             string.IsNullOrWhiteSpace(UserName) ||
                             string.IsNullOrWhiteSpace(PhoneNumber)) &&
                             Password.Equals(ConfirmPassword);
                });
        }
        #endregion
    }
}
