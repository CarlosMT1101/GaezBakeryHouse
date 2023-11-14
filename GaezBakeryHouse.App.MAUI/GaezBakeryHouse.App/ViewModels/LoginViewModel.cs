﻿using Controls.UserDialogs.Maui;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Refit;
using System.Windows.Input;

namespace GaezBakeryHouse.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string _email;
        private string _password;
        private AuthService _authService;
        private IUserDialogs _userDialogs;
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
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            _userDialogs = new UserDialogsImplementation();
            _authService = new AuthService();
            
            OnLoginClickedCommand = new Command(
                execute: async () => await Login(),
                canExecute: () => !(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password)));

            OnRegisterClickedCommand = new Command(
                execute: async () => await App.Current.MainPage.Navigation.PushAsync(new RegisterPage()),
                canExecute: () => true);
        }
        #endregion

        #region Functions
        private async Task Login()
        {
            _userDialogs.ShowLoading(Constants.LoadingMessage);

            var userModel = new UserModel { Email = Email, Password = Password };
            var isValid = await _authService.Login(userModel);

            if (!isValid)
            {
                Navigation.InsertPageBefore(new HomePage(), Navigation.NavigationStack[0]);
                await Navigation.PopAsync();
            }
            else
            {
                await _userDialogs.AlertAsync(
                    Constants.ErrorMessage,
                    Constants.ErrorTitle,
                    Constants.Ok);
            }

            _userDialogs.HideHud();
        }
        #endregion
    }
}