using GaezBakeryHouse.App.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace GaezBakeryHouse.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            var accessToken = SecureStorage.GetAsync("AccessToken").Result;
            var expirationToken = SecureStorage.GetAsync("ExpirationToken").Result;

            if (accessToken != null)
            {
                var expirationDate = DateTime.Parse(expirationToken);

                if (DateTime.UtcNow < expirationDate)
                {
                    await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}");
                }
                else
                {
                    SecureStorage.Remove("AccessToken");
                    SecureStorage.Remove("ExpirationToken");

                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {
            var accessToken = SecureStorage.GetAsync("AccessToken").Result;
            var expirationToken = SecureStorage.GetAsync("ExpirationToken").Result;

            if (accessToken != null)
            {
                var expirationDate = DateTime.Parse(expirationToken);

                if (DateTime.UtcNow < expirationDate)
                {
                    await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}");
                }
                else
                {
                    SecureStorage.Remove("AccessToken");
                    SecureStorage.Remove("ExpirationToken");

                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }
}
