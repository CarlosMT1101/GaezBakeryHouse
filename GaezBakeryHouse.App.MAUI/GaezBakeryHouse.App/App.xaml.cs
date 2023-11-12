using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Views;

namespace GaezBakeryHouse.App
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }
        }

        protected override void OnStart()
        {
            //var accessToken = await SecureStorage.GetAsync(Constants.AccessToken);
            //var expirationToken = await SecureStorage.GetAsync(Constants.ExpirationToken);

            //if (accessToken != null)
            //{
            //    var expirationDate = DateTime.Parse(expirationToken);

            //    if (DateTime.UtcNow < expirationDate)
            //    {
            //        MainPage = new NavigationPage(new HomePage());
            //    }
            //    else
            //    {
            //        App.RemoveUserInformation();
            //        MainPage = new NavigationPage(new LoginPage());
            //    }
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //}
        }

        public static async void SaveUserInformation(AuthResponseModel authResponseModel)
        {
            await Task.WhenAll
            (
                SecureStorage.SetAsync(Constants.FullName, $"{authResponseModel.FullName}"),
                SecureStorage.SetAsync(Constants.PhoneNumber, $"{authResponseModel.PhoneNumber}"),
                SecureStorage.SetAsync(Constants.LastName, $"{authResponseModel.LastName}"),
                SecureStorage.SetAsync(Constants.UserName, $"{authResponseModel.UserName}"),
                SecureStorage.SetAsync(Constants.AccessToken, $"{Constants.Bearer} {authResponseModel.Token}"),
                SecureStorage.SetAsync(Constants.ExpirationToken, authResponseModel.Expiration.ToString()),
                SecureStorage.SetAsync(Constants.ApplicationUserId, authResponseModel.ApplicationUserId)
            );
            
        }

        public static void RemoveUserInformation()
        {
            SecureStorage.Remove(Constants.UserName);
            SecureStorage.Remove(Constants.PhoneNumber);
            SecureStorage.Remove(Constants.FullName);
            SecureStorage.Remove(Constants.LastName);
            SecureStorage.Remove(Constants.AccessToken);
            SecureStorage.Remove(Constants.ExpirationToken);
            SecureStorage.Remove(Constants.ApplicationUserId);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}