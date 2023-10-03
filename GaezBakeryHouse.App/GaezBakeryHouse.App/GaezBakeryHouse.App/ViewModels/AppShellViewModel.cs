using GaezBakeryHouse.App.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand OnLogoutClikedCommand { get; private set; }

        public AppShellViewModel()
        {
            OnLogoutClikedCommand = new Command(
                execute: async () => await Logout(),
                canExecute: () => true);
        }
       
        async Task Logout()
        {
            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("ExpirationToken");
            SecureStorage.Remove("ApplicationUserId");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
