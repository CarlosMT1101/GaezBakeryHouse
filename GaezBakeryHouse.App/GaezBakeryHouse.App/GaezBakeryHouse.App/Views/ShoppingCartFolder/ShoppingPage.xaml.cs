using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShoppingCartPage : ContentPage
	{
        private ShoppingCartViewModel _viewModel;
        private StackLayout _stackLayout;

        public ShoppingCartPage()
		{
			InitializeComponent();
            GetLayout();

            BindingContext = _viewModel = new ShoppingCartViewModel(_stackLayout);
        }

        // *** WARNING ***
        // If you modify the ShoppingCartPages, it is very likely
        // that this method will stop working and cause some
        // exception.
        void GetLayout()
        {
            var scrollView = (ScrollView)shoppingCartItemsView.Children[0];
            var stackLayout = (StackLayout)scrollView.Children[0];
            _stackLayout = stackLayout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}