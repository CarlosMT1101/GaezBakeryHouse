using Acr.UserDialogs;
using FFImageLoading.Forms;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Interfaces;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel, IRefresh
    {
        #region ATRIBUTES
        readonly ShoppingService _shoppingService;
        decimal _totalAmount;
        StackLayout _stackLayout;
        #endregion
        #region PROPERTIES
        public AwesomeObservableCollection<ShoppingCartItemModel> ShoppingCartItemsList { get; private set; }
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
                ((Command)OnContinueClickedCommand).ChangeCanExecute();
            }
        }
        #endregion
        #region COMMANDS
        public ICommand OnContinueClickedCommand { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public ShoppingCartViewModel(StackLayout stackLayout)
        {
            Title = "Carrito";
            _stackLayout = stackLayout;

            _shoppingService = new ShoppingService();
            ShoppingCartItemsList = new AwesomeObservableCollection<ShoppingCartItemModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnContinueClickedCommand = new Command(
                execute: async () => await Shell.Current.GoToAsync($"{nameof(OrderDetailPage)}"),
                canExecute: () => ShoppingCartItemsList.Count() > 0);
        }
        #endregion
        #region FUNCTIONS
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadShoppingCartItems();
            TotalAmount = await _shoppingService.GetUserTotalAmount();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        private async Task LoadShoppingCartItems()
        {
            var shopItems = await _shoppingService.GetShoppingCartItemsByUserId();

            _stackLayout.Children.Clear();

            ShoppingCartItemsList.ClearRange();
            ShoppingCartItemsList.AddRange(shopItems);

            foreach(var item in ShoppingCartItemsList)
                _stackLayout.Children.Add(DrawProductInCart(item));
        }
        private async Task DeleteShoppingCartItem(int id, int productId)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;

            var isDeleted = await _shoppingService.DeleteShoppingCartItem(id, productId);

            if(isDeleted)
            {
                await LoadShoppingCartItems();
                TotalAmount = await _shoppingService.GetUserTotalAmount();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Ocurrío un error", "Mensaje", "Ok");
            }

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        } 
        

        // *** WARNING ***
        // If you modify the ShoppingCartPages, it is very likely
        // that this method will stop working and cause some
        // exception.
        private Frame DrawProductInCart(ShoppingCartItemModel shoppingCartItemModel)
        {
            var frame = new Frame
            {
                HasShadow = false,
                Padding = 10,
                HeightRequest = 100,
                CornerRadius = 10
            };

            var grid = new Grid { ColumnSpacing = 10 };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

            var cachedImage = new CachedImage
            {
                Source = shoppingCartItemModel.ImageSource,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var stackLayout = new StackLayout();

            stackLayout.Children.Add(
                new Label
                {
                    FontAttributes = FontAttributes.None,
                    TextColor = Color.Black,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    Text = shoppingCartItemModel.ProductName
                });

            stackLayout.Children.Add(
                new Label
                {
                    TextColor = Color.Black,
                    FormattedText = new FormattedString 
                    {
                        Spans =
                        {
                            new Span { Text = shoppingCartItemModel.Quantity.ToString() },
                            new Span { Text = " X " },
                            new Span { Text = shoppingCartItemModel.Price.ToString() }
                        }
                    }
                });

            var deleteLabel = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                FontAttributes = FontAttributes.None,
                TextColor = (Color)Application.Current.Resources["PrimaryColor"],
                Text = "Eliminar"
            };
            stackLayout.Children.Add(deleteLabel);

            TouchEffect.SetNativeAnimation(deleteLabel, true);

            Grid.SetColumn(cachedImage, 0);
            Grid.SetColumn(stackLayout, 1);

            grid.Children.Add(cachedImage);
            grid.Children.Add(stackLayout);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.NumberOfTapsRequired = 1;

            tapGestureRecognizer.Tapped += async (object sender, EventArgs e) =>
                await DeleteShoppingCartItem(shoppingCartItemModel.Id, shoppingCartItemModel.ProductId);

            deleteLabel.GestureRecognizers.Add(tapGestureRecognizer);

            frame.Content = grid;

            return frame;
        }
        #endregion
    }
}
