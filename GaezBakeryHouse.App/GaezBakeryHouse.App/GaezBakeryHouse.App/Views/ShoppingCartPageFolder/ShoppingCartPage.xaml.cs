using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCartPage : ContentPage
    {
        ShoppingCartViewModel _viewModel;

        public ShoppingCartPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ShoppingCartViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}