using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomeViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadData();
        }
    }
}