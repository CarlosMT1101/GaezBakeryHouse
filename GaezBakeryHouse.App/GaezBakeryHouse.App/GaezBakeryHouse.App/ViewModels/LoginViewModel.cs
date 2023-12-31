﻿using Acr.UserDialogs;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Refit;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string _email;
        private string _password;
        private IAuthService _authService;
        #endregion

        #region Properties
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

        #region Commands
        public ICommand OnLoginClickedCommand { get; private set; }

        public ICommand OnRegisterClickedCommand { get; private set; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            _authService = RestService.For<IAuthService>(Constants.Url);

            OnLoginClickedCommand = new Command(
                execute: async () => await Login(),
                canExecute: () => !(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password)));

            OnRegisterClickedCommand = new Command(
                execute: async () => await Shell.Current.GoToAsync($"{nameof(RegisterPage)}"),
                canExecute: () => true);
        }
        #endregion

        #region Functions
        private async Task Login()
        {
            UserDialogs.Instance.ShowLoading("Cargando");

            try
            {
                var authRequestModel = CreateAuthRequestModel();
                var response = await _authService.Login(authRequestModel);

                if(response != null)
                {
                    App.SaveUserInformation(response);
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(
                        Constants.ErrorMessage,
                        Constants.ErrorTitle,
                        Constants.Ok);
                }
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync(
                        Constants.ErrorMessage,
                        Constants.ErrorTitle,
                        Constants.Ok);
            }

            UserDialogs.Instance.HideLoading();
        }

        private AuthRequestModel CreateAuthRequestModel() =>
            new AuthRequestModel
            {
                Email = Email,
                Password = Password
            };
        #endregion
    }
}
