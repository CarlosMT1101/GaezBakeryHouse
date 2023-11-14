using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Views;

namespace GaezBakeryHouse.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected async override void OnStart()
        {
            var accessToken = SecureStorage.GetAsync(Constants.AccessToken).Result;
            var expirationToken = SecureStorage.GetAsync(Constants.ExpirationToken).Result;

            if (accessToken != null)
            {
                var expirationDate = DateTime.Parse(expirationToken);

                if (DateTime.UtcNow < expirationDate)
                {
                    var currentPage = MainPage.Navigation.NavigationStack[0];
                    MainPage.Navigation.InsertPageBefore(new HomePage(), currentPage);

                    await MainPage.Navigation.PopAsync();
                }
                else
                {
                    App.RemoveUserInformation();
                }
            }
        }

        public static async void SaveUserInformation(UserResponseModel authResponseModel)
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