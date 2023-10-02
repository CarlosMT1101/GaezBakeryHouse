using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App.Views.CategorySelectedPageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategorySelectedPage : ContentPage
    {
        CategorySelectedViewModel _viewModel;

        public CategorySelectedPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CategorySelectedViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDataAsync();
        }
    }
}