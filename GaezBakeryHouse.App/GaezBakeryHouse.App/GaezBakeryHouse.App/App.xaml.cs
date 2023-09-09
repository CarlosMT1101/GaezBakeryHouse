using GaezBakeryHouse.App.Views;
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

            if(accessToken != null)
            {
                await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
