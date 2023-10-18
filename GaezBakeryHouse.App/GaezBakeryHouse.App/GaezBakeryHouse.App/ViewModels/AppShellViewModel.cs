using GaezBakeryHouse.App.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Commands
        public ICommand OnLogoutClikedCommand { get; private set; }
        #endregion

        #region Constructor
        public AppShellViewModel()
        {
            OnLogoutClikedCommand = new Command(
                execute: async () => await Logout(),
                canExecute: () => true);
        }
        #endregion

        #region Functions
        private async Task Logout()
        {
            App.RemoveUserInformation();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        #endregion
    }
}
