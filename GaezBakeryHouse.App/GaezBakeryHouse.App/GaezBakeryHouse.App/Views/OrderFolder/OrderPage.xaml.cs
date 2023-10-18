using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderPage : ContentPage
	{
		private readonly OrdersViewModel _viewModel;

		public OrderPage ()
		{
			InitializeComponent();

			BindingContext = _viewModel = new OrdersViewModel(
				(StackLayout)orderView.Children[0]);
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
			await _viewModel.LoadDataAsync();
        }
    }
}