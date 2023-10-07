using GaezBakeryHouse.App.ViewModels;
using GaezBakeryHouse.App.Views.HomePageFolder;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCartPage : ContentPage
    {
        ShoppingCartViewModel _viewModel;
        StackLayout _stackLayout;

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
            var refreshView = (RefreshView) shoppingCartItemsView.Children[0];
            var scrollView = (ScrollView) refreshView.Children[0];
            _stackLayout = scrollView.FindByName<StackLayout>("stackLayout");
        }
            

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}