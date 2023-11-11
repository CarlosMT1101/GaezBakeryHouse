using GaezBakeryHouse.App.Views;
using Xamarin.Forms;


namespace GaezBakeryHouse.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        //protected async override void OnStart()
        //{
        //    var accessToken = await SecureStorage.GetAsync(Constants.AccessToken);
        //    var expirationToken = await SecureStorage.GetAsync(Constants.ExpirationToken);

        //    if (accessToken != null)
        //    {
        //        var expirationDate = DateTime.Parse(expirationToken);

        //        if (DateTime.UtcNow < expirationDate)
        //        {
        //            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        //        }
        //        else
        //        {
        //            App.RemoveUserInformation();
        //            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        //        }
        //    }
        //    else
        //    {
        //        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        //    }
        //}

        //public static async void SaveUserInformation(AuthResponseModel authResponseModel)
        //{
        //    await SecureStorage.SetAsync(Constants.FullName, $"{authResponseModel.FullName}");
        //    await SecureStorage.SetAsync(Constants.PhoneNumber, $"{authResponseModel.PhoneNumber}");
        //    await SecureStorage.SetAsync(Constants.LastName, $"{authResponseModel.LastName}");
        //    await SecureStorage.SetAsync(Constants.UserName, $"{authResponseModel.UserName}");
        //    await SecureStorage.SetAsync(Constants.AccessToken, $"{Constants.Bearer} {authResponseModel.Token}");
        //    await SecureStorage.SetAsync(Constants.ExpirationToken, authResponseModel.Expiration.ToString());
        //    await SecureStorage.SetAsync(Constants.ApplicationUserId, authResponseModel.ApplicationUserId);
        //}

        //public static void RemoveUserInformation()
        //{
        //    SecureStorage.Remove(Constants.UserName);
        //    SecureStorage.Remove(Constants.PhoneNumber);
        //    SecureStorage.Remove(Constants.FullName);
        //    SecureStorage.Remove(Constants.LastName);
        //    SecureStorage.Remove(Constants.AccessToken);
        //    SecureStorage.Remove(Constants.ExpirationToken);
        //    SecureStorage.Remove(Constants.ApplicationUserId);
        //}

        //protected override void OnSleep()
        //{
        //}

        //protected async override void OnResume()
        //{
        //    var accessToken = await SecureStorage.GetAsync(Constants.AccessToken);
        //    var expirationToken = await SecureStorage.GetAsync(Constants.ExpirationToken);

        //    if (accessToken != null)
        //    {
        //        var expirationDate = DateTime.Parse(expirationToken);

        //        if (DateTime.UtcNow < expirationDate)
        //        {
        //            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        //        }
        //        else
        //        {
        //            App.RemoveUserInformation();
        //            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        //        }
        //    }
        //    else
        //    {
        //        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        //    }
        //}
    }
}
