﻿using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategorySelectedPage : ContentPage
	{
        private StackLayout _leftStackLayout;
        private StackLayout _rightStackLayout;
        private readonly CategorySelectedViewModel _viewModel;

        public CategorySelectedPage()
		{
			InitializeComponent();
            GetLayouts();

            BindingContext = _viewModel = new CategorySelectedViewModel(_leftStackLayout, _rightStackLayout);
        }

        // *** WARNING ***
        // If you modify the HomePageViews, it is very likely
        // that this method will stop working and cause some
        // exception.
        void GetLayouts()
        {
            var stackLayout = (StackLayout)productCategoryView.Children[0];
            var grid = (Grid)stackLayout.Children[0];

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