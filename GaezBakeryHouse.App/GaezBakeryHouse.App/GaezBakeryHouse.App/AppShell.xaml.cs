using GaezBakeryHouse.App.ViewModels;
using GaezBakeryHouse.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaezBakeryHouse.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(CategorySelectedPage), typeof(CategorySelectedPage));
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
        }
    }
}