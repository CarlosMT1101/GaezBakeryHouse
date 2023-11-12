using GaezBakeryHouse.App.ViewModels;

namespace GaezBakeryHouse.App.Views;

public partial class LoginPage : ContentPage
{
	private readonly LoginViewModel _viewModel;

	public LoginPage()
	{
		InitializeComponent();
		BindingContext = _viewModel = new LoginViewModel();
	}
}