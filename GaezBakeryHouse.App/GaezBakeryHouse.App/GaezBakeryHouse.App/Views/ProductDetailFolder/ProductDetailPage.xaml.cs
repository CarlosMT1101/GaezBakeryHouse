using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductDetailPage : ContentPage
	{
		private readonly ProductDetailViewModel _viewModel;

		public ProductDetailPage ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new ProductDetailViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}