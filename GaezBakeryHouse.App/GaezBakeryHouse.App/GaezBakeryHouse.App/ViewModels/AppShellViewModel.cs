using GaezBakeryHouse.App.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand OnLogoutClikedCommand => new Command(async () =>
        {
            SecureStorage.Remove("AccessToken");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        });
    }
}
