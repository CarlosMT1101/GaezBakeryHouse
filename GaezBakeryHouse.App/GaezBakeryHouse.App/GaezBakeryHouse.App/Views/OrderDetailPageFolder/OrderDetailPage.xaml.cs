using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        readonly OrderDetailViewModel _viewModel;

        public OrderDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new OrderDetailViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}