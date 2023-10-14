using GaezBakeryHouse.App.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        private StackLayout _leftStackLayout;
        private StackLayout _rightStackLayout;

        public HomePage()
        {
            InitializeComponent();
            GetLayouts();

            BindingContext = _viewModel = new HomeViewModel(_leftStackLayout, _rightStackLayout);
        }

        // *** WARNING ***
        // If you modify the HomePageViews, it is very likely
        // that this method will stop working and cause some
        // exception.
        private void GetLayouts()
        {
            var stackLayout = (StackLayout)trendingProductsView.Children[0];
            var grid = (Grid)stackLayout.Children[1];

            _leftStackLayout = grid.FindByName<StackLayout>("leftStackLayout");
            _rightStackLayout = grid.FindByName<StackLayout>("rightStackLayout");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}