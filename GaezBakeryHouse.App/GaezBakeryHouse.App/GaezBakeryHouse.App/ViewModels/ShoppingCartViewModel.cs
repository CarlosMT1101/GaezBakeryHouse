using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using Refit;
using System.Threading.Tasks;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using FFImageLoading.Forms;
using Xamarin.CommunityToolkit.Effects;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using GaezBakeryHouse.App.Views;

namespace GaezBakeryHouse.App.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        #region Attributes
        private readonly IShoppingCartItemService _shoppingService;
        decimal _totalAmount;
        StackLayout _stackLayout;
        #endregion

        #region Properties
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

        public AwesomeObservableCollection<ShoppingCartItemModel> ShoppingCartItems { get; private set; }
        #endregion

        #region Commands
        public ICommand OnContinueClickedCommand { get; private set; }
        #endregion

        #region Constructor
        public ShoppingCartViewModel(StackLayout stackLayout)
        {
            Title = "Mi carrito";
            _stackLayout = stackLayout;

            _shoppingService = RestService.For<IShoppingCartItemService>(Constants.Url);
            ShoppingCartItems = new AwesomeObservableCollection<ShoppingCartItemModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnContinueClickedCommand = new Command(
                execute:  async () => await Shell.Current.GoToAsync($"{nameof(OrderDetailPage)}"),
                canExecute: () => ShoppingCartItems.Count() > 0);
        }
        #endregion

        #region Functions
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                await LoadShoppingCartItems();
                await LoadTotalAmount();
                OnSuccessTask();
            }
            catch (Exception)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task LoadShoppingCartItems()
        {
            var shopItems = await _shoppingService.GetShoppingCartItemsByUserId(AccessToken, ApplicationUserId);

            _stackLayout.Children.Clear();

            ShoppingCartItems.ClearRange();
            ShoppingCartItems.AddRange(shopItems);

            LoadProductImages();
            DrawProductsInLayouts();
        }

        private void LoadProductImages()
        {
            foreach (var product in ShoppingCartItems)
            {
                product.ImageSource = ImageSource.FromStream(() => new MemoryStream(product.ProductImage));
            }
        }

        private void DrawProductsInLayouts()
        {
            foreach (var item in ShoppingCartItems)
            {
                _stackLayout.Children.Add(DrawProductInCart(item));
            }
        }

        private async Task DeleteShoppingCartItem(int id, int productId)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            OnLoadingTask();

            try
            {
                var response = await _shoppingService.DeleteShoppingCartItem(AccessToken, id, productId, ApplicationUserId);

                if (response.IsSuccessStatusCode)
                {
                    await LoadShoppingCartItems();
                    await LoadTotalAmount();
                    OnSuccessTask();
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                OnErrorTask();

                await UserDialogs.Instance.AlertAsync(
                    Constants.ErrorMessage,
                    Constants.ErrorTitle,
                    Constants.Ok);
            }

            UserDialogs.Instance.HideLoading();
        }

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

        private async Task LoadTotalAmount()
        {
            var response = await _shoppingService.GetUserTotalAmount(AccessToken, ApplicationUserId);
            var responseContent = await response.Content.ReadAsStringAsync();

            TotalAmount = decimal.Parse(responseContent.Replace('.', ','));
        }
        #endregion
    }
}
